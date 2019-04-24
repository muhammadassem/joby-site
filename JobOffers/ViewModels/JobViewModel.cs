using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModels
{
    public class JobViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "Job Title")]
        [Required]
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        [Required]
        public string JobDescription { get; set; }
        [Display(Name = "Image Url")]
        [Required]
        public string ImageUrl { get; set; }

        //[ValidateFile(ErrorMessage = "Image is required")]
        //public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public JobCategory JobCategory { get; set; }

        public IEnumerable<JobCategory> JobCategories { get; set; }

        public JobViewModel()
        {
            Id = 0;
        }
        public JobViewModel(Job job)
        {
            Id = job.Id;
            JobTitle = job.JobTitle;
            JobDescription = job.JobDescription;
            ImageUrl = job.ImageUrl;
            CategoryId = job.CategoryId;
            JobCategory = job.JobCategory;
        }
    }
}