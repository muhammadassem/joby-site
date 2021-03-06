﻿using JobOffers.Models;
using JobOffers.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobOffers.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private ApplicationDbContext _context;

        public JobController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Job
        public ActionResult Index(string filter)
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            var jobs = _context.Jobs.Include(j => j.JobCategory).Include(j => j.User).ToList();
            return View(jobs);
        }

        public ActionResult New()
        {
            var jobCategoriesList = _context.JobCategories.ToList();

            var jobFormViewModel = new JobViewModel
            {
                JobCategories = jobCategoriesList
            };

            return View("JobForm", jobFormViewModel);
        }

        public ActionResult Edit(int? id)
        {
            var job = _context.Jobs.SingleOrDefault(j => j.Id == id);

            if (job == null)
            {
                return HttpNotFound();
            }

            var jobCategoriesList = _context.JobCategories.ToList();

            var jobFormViewModel = new JobViewModel(job)
            {
                JobCategories = jobCategoriesList
            };

            return View("JobForm", jobFormViewModel);
        }

        [HttpPost]
        public ActionResult Save(Job model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                var jobCategories = _context.JobCategories.ToList();

                var viewModel = new JobViewModel(model)
                {
                    JobCategories = jobCategories
                };

                return View("JobForm", viewModel);
            }

            if (upload != null)
            {
                if (model.Id != 0)
                {
                    // Delete updated photo from server
                    var oldImagePath = model.ImageUrl;
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), oldImagePath));
                }
                var fileName = Path.GetFileNameWithoutExtension(upload.FileName) + DateTime.Now.ToLongDateString() + Path.GetExtension(upload.FileName);

                string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                upload.SaveAs(path);
                model.ImageUrl = fileName;
            }

            if (model.Id == 0)
            {
                model.PublisherId = User.Identity.GetUserId();
                _context.Jobs.Add(model);
            }
            else
            {
                var jobInDb = _context.Jobs.SingleOrDefault(j => j.Id == model.Id);

                if (jobInDb == null)
                {
                    return HttpNotFound();
                }

                jobInDb.JobTitle = model.JobTitle;
                jobInDb.JobDescription = model.JobDescription;
                jobInDb.ImageUrl = model.ImageUrl;
                jobInDb.CategoryId = model.CategoryId;

            }

            _context.SaveChanges();

            return RedirectToAction("ViewAll", "Job");
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var job = _context.Jobs.SingleOrDefault(j => j.Id == id);

            if (job == null)
            {
                return HttpNotFound();
            }

            _context.Jobs.Remove(job);
            _context.SaveChanges();

            return RedirectToAction("ViewAll", "Job");
        }

        public ActionResult Details(int id)
        {
            var jobInDb = _context.Jobs.Include(j => j.JobCategory).SingleOrDefault(j => j.Id == id);

            if (jobInDb == null)
            {
                return HttpNotFound();
            }

            var viewModel = new JobViewModel(jobInDb);
            Session["jobId"] = id;

            return View(viewModel);
        }

        [Authorize]
        public ActionResult ApplyForJob()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyForJob(ApplyForJob model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ApplyForJobViewModel
                {
                    Message = model.Message
                };

                return View(viewModel);
            }

            model.JobId = (int)Session["jobId"];
            model.ApplicatorId = User.Identity.GetUserId();

            var checkIfSameUserApplicateForSameJob = _context.ApplyForJob.Where(j => j.ApplicatorId == model.ApplicatorId && j.JobId == model.JobId).ToList();

            if (checkIfSameUserApplicateForSameJob.Count >= 1)
            {
                ViewBag.message = "Already applied for this job";
                return View();
            }

            model.ApplicationDate = DateTime.Now;

            _context.ApplyForJob.Add(model);
            _context.SaveChanges();
            ViewBag.message = "Applied Successfully";

            return RedirectToAction("Index", "Job");
        }
    }
}