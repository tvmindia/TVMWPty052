using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter product name")]
        [Display(Name = "Product Name")]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter short description")]
        [Display(Name = "Short Description")]
        [MaxLength(250)]
        public string ShortDescription { get; set; }
        
        [Required(ErrorMessage = "Please enter SKU")]
        [Display(Name = "SKU")]
        public string SKU { get; set; }
        [Required(ErrorMessage = "Please enter Enabled")]
        [Display(Name = "Active(Yes/No)")]
        public Boolean Enabled { get; set; }
        [Required(ErrorMessage = "Please enter Unit")]
        [Display(Name = "Unit")]
        public string Unit { get; set; }
       
        [Display(Name = "Image URL")]
        public string URL { get; set; }
        [Required(ErrorMessage = "Please enter Action Type")]
        [Display(Name = "Action Type")]
        public char ActionType { get; set; }//book //buy //Quote
        [Required(ErrorMessage = "Please enter Supplier")]
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        
        [Display(Name = "Manufacturer")]
        public int ManufacturerID { get; set; }
        [Required(ErrorMessage = "Please enter Product Type")]
        [Display(Name = "Product Type")]
        public char ProductType { get; set; }//simple //configurable

       
        [Display(Name = "Attribute Set")]
        public int AttributeSetID { get; set; }

        [Required(ErrorMessage = "Please enter Free Delivery")]
        [Display(Name = "Free Delivery(Y/N)")]
        public Boolean FreeDelivery { get; set; }

        [Display(Name = "Search Tags")]
        public string HeaderTags { get; set; }

        public CommonViewModel commonObj { get; set; }
      

    }
}