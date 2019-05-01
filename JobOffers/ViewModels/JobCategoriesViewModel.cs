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
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job Category Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Job Category Description")]
        public string Description { get; set; }

        public JobCategoriesViewModel(JobCategory model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }
    }
}