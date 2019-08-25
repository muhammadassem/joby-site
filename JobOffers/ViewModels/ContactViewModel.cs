using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JobOffers.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        public string Name { get; set; }

        [StringLength(1000)]
        [Required]
        public string Subject { get; set; }

        [StringLength(5000)]
        [Required]
        [AllowHtml]
        public string Message { get; set; }
    }
}