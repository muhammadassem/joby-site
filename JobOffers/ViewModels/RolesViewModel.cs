using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffers.ViewModels
{
    public class RolesViewModel
    {
        
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public RolesViewModel()
        {
        }

        public RolesViewModel(IdentityRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}