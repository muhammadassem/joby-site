using JobOffers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModels
{
    public class AppliedJobsPostedByPublisherViewModel
    {
        public string JobTitle { get; set; }

        public ICollection<ApplyForJob> AppliedJobs { get; set; }
    }
}