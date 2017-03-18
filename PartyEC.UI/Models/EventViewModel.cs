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
        public string RelatedCategoriesCSV { get; set; }

        [Display(Name = "Related Categories")]
        public List<CategoriesViewModel> CategoryList { get; set; }

        [Display(Name = "Event Image")]
        public string EventImageID { get; set; }
        public string URL { get; set; }
        public LogDetailsViewModel commonObj { get; set; }




    }
}