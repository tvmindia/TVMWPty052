using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
    }
}