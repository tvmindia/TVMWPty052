using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class EventViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Attribute name")]
        [Display(Name = "Name")]
        [MaxLength(50)]
        [RegularExpression("^([a-zA-Z9 .&'-]+)$", ErrorMessage = "Invalid Attribute Name")]
        public string Name { get; set; }

        [Display(Name = "Related Categories")]
        public string RelatedCategories { get; set; }

        [Display(Name = "Related Categories")]
        public List<CategoriesViewModel> CategoryList { get; set; }

        [Display(Name = "Event Image")]
        public string EventImage { get; set; }

        public CommonViewModel commonObj { get; set; }




    }
}