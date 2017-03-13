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

        public string Name { get; set; }

        public bool Checked  { get; set; }


    }
}