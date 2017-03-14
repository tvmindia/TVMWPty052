﻿using System;
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
        public bool Filter { get; set; }
        public bool Navigation { get; set; }
        [Display(Name = "Description")]
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool Enable { get; set; }
        public float CategoryOrder { get; set; }
        public string ImageID { get; set; }
        public bool Checked  { get; set; }


    }
}