using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class EventRequestsViewModel
    {
       
        public int ID { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Event Request No")]
        public string EventReqNo { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Event Type")]
        public int EventTypeID { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Event Title")]
        public string EventTitle { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Event Date")]
        public DateTime EventDateTime { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Event Status")]
        public int EventStatus { get; set; }



        [Display(Name = "Customer ID")]
        [ScaffoldColumn(true)]
        public int CustomerID { get; set; }



        [ReadOnly(true)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Contact Type")]
        public string ContactType { get; set; }


        [ReadOnly(true)]
        [Display(Name = "Looking For")]
        public string LookingFor { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Specification")]
        public string RequirementSpec { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Customer Message")]
        public string Message { get; set; }

        [ReadOnly(true)]
        [Display(Name = "No Of Persons")]
        public int NoOfPersons { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Budget")]
        public decimal Budget { get; set; }

     
     

        [Display(Name = "Admin Remarks")]
        public string AdminRemarks { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Must be a Date")]
        [Display(Name = "FollowUp Date")]
        public DateTime? FollowUpDate { get; set; }



        [Display(Name = "Currency Code")]
        public string CurrencyCode { get; set; }

        [Display(Name = "Currency Rate")]
        public decimal CurrencyRate { get; set; }

        [Display(Name = "Agreed Amount")] 
        public decimal TotalAmt { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal TotalTaxAmt { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal TotalDiscountAmt { get; set; }

        public LogDetailsViewModel logDetailsObj { get; set; } 
    }
}