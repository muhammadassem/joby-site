using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModels
{
    public class JobCategoriesViewModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Job Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string Description { get; set; }

        public string PageTitle
        { get
            {
                return (Id == 0) ? "Add New Job Category" : "Edit Job Category";
            }
        }

        public JobCategoriesViewModel()
        {
            Id = 0;
        }

        public JobCategoriesViewModel(JobCategories model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }
    }
}