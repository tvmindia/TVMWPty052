using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class CategoriesViewModel
    {
        public int ID { get; set; }
        public int ParentID { get; set; }

        [Required(ErrorMessage = "Please Enter Category name")]
        [Display(Name = "Name")]
        [MaxLength(50)]
        [RegularExpression("^([a-zA-Z9 .&'-]+)$", ErrorMessage = "Invalid Category Name")]
        public string Name { get; set; }

        public bool System { get; set; }
        public int ChildrenCount { get; set; }
        [Display(Name= "Use for Filter (Yes/No)")]
        public bool Filter { get; set; }
        [Display(Name = "Use for Navigation (Yes/No)")]
        public bool Navigation { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name="Is Active")]
        public bool Enable { get; set; }
        public float CategoryOrder { get; set; }
        public string ImageID { get; set; }
        public bool Checked  { get; set; }
        public string URL { get; set; }
        public string TableDataAdd { get; set; }
        public string TableDataDelete { get; set; }
        public float PositionNo { get; set; }
        public int ProductID { get; set; }
        public HttpPostedFile CategoryImageUpload { get; set; }
        public LogDetailsViewModel commonObj { get; set; }


    }

    public class CategoriesListAppViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
    public class TopProductsOfCategoryAppViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
    }
}