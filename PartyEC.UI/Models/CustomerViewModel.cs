using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class CustomerViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        public string Mobile { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public int ProfileImageID { get; set; }

        public int OrdersCount { get; set; }

        public int BookingsCount { get; set; }

        public int QuotationsCount { get; set; }

        public int OrdersCountHistory { get; set; }

        public int BookingsCountHistory { get; set; }

        public int QuotationsCountHistory { get; set; }


        public LogDetailsViewModel logDetailsObj { get; set; }
    }
}