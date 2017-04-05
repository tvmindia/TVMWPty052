using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

       

        public Guid? ProfileImageID { get; set; }

        public int? OrdersCount { get; set; }

        public int? BookingsCount { get; set; }

        public int? QuotationsCount { get; set; }

        public int? OrdersCountHistory { get; set; }

        public int? BookingsCountHistory { get; set; }

        public int? QuotationsCountHistory { get; set; }

        public bool IsActive { get; set; }
        public CustomerAddressViewModel customerAddress { get; set; }
        public LogDetailsViewModel logDetailsObj { get; set; }
    }
    public class CustomerAddressViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public string CountryCode { get; set; }
        public CountryViewModel country { get; set; }
        public string StateProvince { get; set; }
        public string ContactNo { get; set; }
        public bool BillDefaultYN { get; set; }
        public bool ShipDefaultYN { get; set; }
        public LogDetailsViewModel logDetailsObj { get; set; }
        



    }
}