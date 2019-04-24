using JobOffers.Models;
using JobOffers.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobOffers.Controllers
{
    public class JobCategoryController : Controller
    {
        private ApplicationDbContext _context;

        public JobCategoryController()
        {
            this._context = new ApplicationDbContext();
        }
        // GET: JobCategory
        public ActionResult Index()
        {
            IEnumerable<JobCategory> jobsView = _context.JobCategories.ToList();

            return View(jobsView);
        }

        public ActionResult Details(int id)
        {

            var category = _context.JobCategories.SingleOrDefault(c => c.Id == id);

            if(category == null)
            {
                return HttpNotFound();
            }

            var viewModel = new JobCategoriesViewModel
            {
                Name = category.Name,
                Description = category.Description
            };

            return View(viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new JobCategoriesViewModel();
            return View("JobCategoriesForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var categoryInDb = _context.JobCategories.SingleOrDefault(c => c.Id == id);

            if(categoryInDb == null)
            {
                return HttpNotFound();
            }

            var viewModel = new JobCategoriesViewModel(categoryInDb); // mapping inside view model

            return View("JobCategoriesForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(JobCategory model)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new JobCategoriesViewModel
                {
                    Name = model.Name,
                    Description = model.Name
                };

                return View("JobCategoriesForm");
            }

            if(model.Id == 0)
            {
                // create
                _context.JobCategories.Add(model);
            } else
            {
                var categoryInDb = _context.JobCategories.SingleOrDefault(m => m.Id == model.Id);

                if (categoryInDb == null)
                {
                    return HttpNotFound();
                }

                categoryInDb.Name = model.Name;
                categoryInDb.Description = model.Description;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "JobCategory");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var categoryInDb = _context.JobCategories.SingleOrDefault(m => m.Id == id);
            
            if(categoryInDb == null)
            {
                return HttpNotFound();
            }
            _context.JobCategories.Remove(categoryInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "JobCategory");
        }
    }
}