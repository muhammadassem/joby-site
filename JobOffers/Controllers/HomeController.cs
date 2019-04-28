using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using JobOffers.ViewModels;

namespace JobOffers.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var JobsList = _dbContext.JobCategories.Include(j => j.Jobs).ToList();

            return View(JobsList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult JobsAppliedByUser()
        {
            var userId = User.Identity.GetUserId();
            var jobs = _dbContext.ApplyForJob.Include(j => j.Job).Include(j => j.User).Where(j => j.ApplicatorId == userId).ToList();
            return View(jobs);
        }

        [Authorize]
        public ActionResult DetailsOfAppliedJob(int id)
        {
            var jobInDb = _dbContext.ApplyForJob.Include(j => j.Job).SingleOrDefault(j => j.Id == id);

            if (jobInDb == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ApplyForJobViewModel(jobInDb);

            return View(viewModel);
        }

        public ActionResult EditAppliedJob(int id)
        {
            var jobInDb = _dbContext.ApplyForJob.Include(m => m.Job).SingleOrDefault(j => j.Id == id);

            if (jobInDb == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ApplyForJobViewModel(jobInDb)
            {
                Job = jobInDb.Job
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditAppliedJob(ApplyForJob job)
        {
           
            if (!ModelState.IsValid)
            {
                var jobInDb = _dbContext.Jobs.SingleOrDefault(j => j.Id == job.JobId);

                var viewModel = new ApplyForJobViewModel(job)
                {
                    Job = jobInDb
                };

                return View(viewModel);
            }

            var jobAppliedFor = _dbContext.ApplyForJob.Include(j => j.Job).SingleOrDefault(j => j.Id == job.Id);

            if(jobAppliedFor == null)
            {
                return HttpNotFound();
            }

            // we can do eihther the nex line of edit properties manually
            //_dbContext.Entry(jobAppliedFor).State = EntityState.Modified;
            jobAppliedFor.Message = job.Message;
            jobAppliedFor.ApplicationDate = DateTime.Now;

            _dbContext.SaveChanges();
            
            return RedirectToAction("JobsAppliedByUser");
        }

        [HttpPost]
        public ActionResult DeleteAppliedJob(int id)
        {
            var jobInDb = _dbContext.ApplyForJob.SingleOrDefault(j => j.Id == id);

            if (jobInDb == null)
            {
                return HttpNotFound();
            }

            _dbContext.ApplyForJob.Remove(jobInDb);
            _dbContext.SaveChanges();

            return RedirectToAction("JobsAppliedByUser");
        }

        public ActionResult AppliedJobsPostedByPublisher()
        {
            var currentUserId = User.Identity.GetUserId();

            var jobs = _dbContext.ApplyForJob.Include("Job").Include("User").ToList();

            var jobsPostedByPublisher = from apJob in jobs
                                        join job in _dbContext.Jobs
                                        on apJob.JobId equals job.Id
                                        where job.PublisherId == currentUserId
                                        select apJob;

            var jobsGrouped = from j in jobs
                              group j by j.Job.JobTitle
                              into gr
                              select new AppliedJobsPostedByPublisherViewModel
                              {
                                  JobTitle = gr.Key,
                                  AppliedJobs = gr.ToList()
                              };

            return View(jobsGrouped.ToList());
        }


        // GET
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string search) // search paramter must be the same name as the name attribute of search input field
        {
            var searchedList = _dbContext.Jobs.Where
                (
                    j => j.JobTitle.Contains(search) ||
                    j.JobDescription.Contains(search) ||
                    j.JobCategory.Name.Contains(search)
                );

            return View(searchedList.ToList());
        }
    }
}