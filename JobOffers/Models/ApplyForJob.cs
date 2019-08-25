using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobOffers.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public Job Job { get; set; }

        public string ApplicatorId { get; set; }

        [ForeignKey("ApplicatorId")]
        public ApplicationUser User { get; set; }
    }
}