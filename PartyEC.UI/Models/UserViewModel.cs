using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter user name")]
        [Display(Name = "Name")]
        [StringLength(250)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter login name")]
        [Display(Name = "Login Name")]
        [StringLength(250)]
        public string LoginName { get; set; }

        [StringLength(250)]
        public string Password { get; set; }
        public int ProfileImageId { get; set; }
        public LogDetailsViewModel logDetails { get; set; }

    }
}