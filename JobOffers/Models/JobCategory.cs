using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class JobCategory
    {
        
        public int Id { get; set; }

        [Required]
        [Display(Name="Job Category Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}