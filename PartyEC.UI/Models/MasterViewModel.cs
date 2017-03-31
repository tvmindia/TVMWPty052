using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class CountryViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class ManufacturerViewModel
    {
        public ManufacturerViewModel()
        {
            country = new CountryViewModel();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public CountryViewModel country { get; set; }
    }

    public class SupplierViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Supplier Name")]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public LogDetailsViewModel commonObj { get; set; }
    }
    public class ShippingLocationViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Location Name")]
        [Display(Name = "Location Name")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public LogDetailsViewModel commonObj { get; set; }
       
    }


    public class OrderStatusViewModel
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}