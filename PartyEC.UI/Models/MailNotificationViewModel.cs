using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class MailNotificationViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please Select Customer from Above")]
        public string CustomerName { get; set; }
        public CustomerViewModel customer { get; set; }
        public string Type { get; set; }
        [Required(ErrorMessage = "Please enter Title  ")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter Message  ")]
        public string Message { get; set; }
        public string CustomerIDList { get; set; }
        public Int16 Status { get; set; }
        public LogDetailsViewModel logDetailsObj { get; set; }
    }
}