using JobOffers.Models;
using JobOffers.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobOffers.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public RolesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Roles
        public ActionResult Index()
        {
            var roles = _dbContext.Roles.ToList();
            return View(roles);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(IdentityRole model)
        {
            if(!ModelState.IsValid)
            {
                return View("New", model);
            }

            //try
            //{
                _dbContext.Roles.Add(model);
                _dbContext.SaveChanges();
            //} catch (EntityException e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            return RedirectToAction("Index");
        }

        // GET: Edit Roles
        public ActionResult Edit(string id)
        {
            var role = _dbContext.Roles.SingleOrDefault(m => m.Id == id);

            if(role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [HttpPost]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(role);
                }
                var roleInDb = _dbContext.Roles.SingleOrDefault(r => r.Id == role.Id);
                roleInDb.Name = role.Name;

                //_dbContext.Entry(role).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Roles");
            } catch
            {
                return View(role);
            }
            
        }

        public ActionResult Details(string id)
        {
            var role = _dbContext.Roles.SingleOrDefault(m => m.Id == id);

            if (role == null)
            {
                return HttpNotFound();
            }

            var viewModel = new RolesViewModel(role);

            return View(viewModel);
        }

        public ActionResult Delete(string id)
        {
            var role = _dbContext.Roles.SingleOrDefault(r => r.Id == id);

            if (role == null)
            {
                return HttpNotFound();
            }

            _dbContext.Roles.Remove(role);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Roles");
        }
    }
}