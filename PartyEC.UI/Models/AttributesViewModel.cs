using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class AttributesViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Attribute name")]
        [Display(Name = "Attribute Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Attribute Caption")]
        [Display(Name = "Attribute Caption")]
        [MaxLength(250)]
        public string Caption { get; set; }


        [Required(ErrorMessage = "Please Enter Attribute Type")]
        [Display(Name = "Attribute Type")]
        [MaxLength(250)]
        public string AttributeType { get; set; }

        [Required(ErrorMessage = "Please Enter Configurable Values")]
        [Display(Name = "Configurable Values  ")]
        [MaxLength(250)]
        public string CSValues { get; set; }

        [Required(ErrorMessage = "Please Enter Entity Type")]
        [Display(Name = "Entity Type  ")]
        [MaxLength(250)]
        public string EntityType { get; set; }


        public bool ConfigurableYN { get; set; }
        public bool FilterYN { get; set; }
        public bool MandatoryYN { get; set; }
        public bool ComparableYN { get; set; }

    }
}