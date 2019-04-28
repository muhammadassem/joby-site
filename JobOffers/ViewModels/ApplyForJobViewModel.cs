using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModels
{
    public class ApplyForJobViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }

        public DateTime? DateAdded { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; }
        public string ApplicatorId { get; set; }

        public ApplyForJobViewModel()
        {
            Id = 0;
        }
        public ApplyForJobViewModel(ApplyForJob job)
        {
            Message = job.Message;
            DateAdded = job.ApplicationDate;
            JobId = job.JobId;
            ApplicatorId = job.ApplicatorId;
        }
    }
}