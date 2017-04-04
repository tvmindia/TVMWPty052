﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Models
{
    public class CountryViewModel
    {
        [Display(Name = "Country")]
        public string Code { get; set; } 
        public string Name { get; set; }
    }
    public class ManufacturerViewModel
    {
        //public ManufacturerViewModel()
        //{
        //    country = new CountryViewModel();
        //}
        public int ID { get; set; }

        public string Name { get; set; }
        public CountryViewModel country { get; set; }
        public List<SelectListItem> CountryList { get; set; }

        public LogDetailsViewModel commonObj { get; set; }
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

    public class SupplierLocationsViewModel
    {
        public int ID { get; set; }

        public int LocationID { get; set; }
        public int SupplierID { get; set; }

        public string SupplierName { get; set; }
        public string LocationName { get; set; }

        public decimal ShippingCharge { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<SelectListItem> LocationList { get; set; }
        public List<SelectListItem> supplierList { get; set; }
        

        public LogDetailsViewModel commonObj { get; set; }

      
    }


    public class OrderStatusViewModel
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}