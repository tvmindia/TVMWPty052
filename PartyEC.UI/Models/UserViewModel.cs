using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter user name")]
        [Display(Name = "Name")]
        [StringLength(250)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Choose Role")]
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        public string RoleList { get; set; }
        //public List<SelectListItem> roles { get; set; }

        [Required(ErrorMessage = "Please enter login name")]
        [Display(Name = "Login Name")]
        [StringLength(250)]
        public string LoginName { get; set; }
        //[Required(ErrorMessage = "Password is required.")]
        [StringLength(250)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [StringLength(250)]
        //[Required(ErrorMessage = "Confirmation Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        public int UserRoleLinkID { get; set; }
        public Guid? ProfileImageId { get; set; }
        public LogDetailsViewModel logDetails { get; set; }
        public RoleViewModel RoleObj { get; set; }
    }
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter login name")]
        //[Display(Name = "Login Name")]
        [StringLength(250)]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        //[Display(Name = "Password")]
        [StringLength(250)]
        public string Password { get; set; }
      
        ////[Required(ErrorMessage = "Confirmation Password is required.")]
        //[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        //public string ConfirmPassword { get; set; }

    }

}