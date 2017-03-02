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
        [Required(ErrorMessage = "Please enter product type")]
        [Display(Name = "product type")]
      
        public char ProductType { get; set; }

        public CommonViewModel commonObj { get; set; }
      

    }
}