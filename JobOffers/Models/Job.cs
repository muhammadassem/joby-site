using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class Job
    {
        [Required]
        public int Id { get; set; }
        [Display(Name="Job Title")]
        [Required]
        public string JobTitle { get; set; }
        [Display(Name ="Job Description")]
        [Required]
        public string JobDescription{ get; set; }
        [Display(Name= "Image Url")]
       
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public JobCategory JobCategory { get; set; }
    }
}