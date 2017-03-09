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


        [Required(ErrorMessage = "Please Enter Data Type")]
        [Display(Name = "Data Type")]
        [MaxLength(250)]
        public string AttributeType { get; set; }

        [Required(ErrorMessage = "Please Enter Configurable Values")]
        [Display(Name = "Configurable Values")]
        [MaxLength(250)]
        public string CSValues { get; set; }

        [Required(ErrorMessage = "Please Enter Entity Type")]
        [Display(Name = "Entity Type")]
        [MaxLength(250)]
        public string EntityType { get; set; }

        [Display(Name = "Attribute Type")]
        public bool ConfigurableYN { get; set; }

        [Display(Name = "Use for Filter")]
        public bool FilterYN { get; set; }

        [Display(Name = "Is Mandatory")]
        public bool MandatoryYN { get; set; }

        [Display(Name = "Is Comparable")]
        public bool ComparableYN { get; set; }

        public CommonViewModel commonObj { get; set; }

    }
    public class AttributeSetViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please Enter Attribute set name")]
        [Display(Name = "Attribute Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        public string TreeList { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
    public class AttributeSetLinkViewModel
    {
        public int ID { get; set; }
        public int AttributeSetID { get; set; }
        public int AttributeID { get; set; }
        public float DisplayOrder { get; set; }
        public CommonViewModel commonObj { get; set; }
    }
}