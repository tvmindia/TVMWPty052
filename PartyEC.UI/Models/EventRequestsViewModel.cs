using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public string EventType { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Event Title")]
        public string EventTitle { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Event Date")]
        public string EventDateTime { get; set; }
        public string EventTime { get; set; }

        [Display(Name = "Event Status")]
        public string EventDesc { get; set; }




        public CustomerViewModel Customerlist { get; set; }

        [Display(Name = "Customer ID")]
        [ScaffoldColumn(true)]
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }



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

        

        [Display(Name = "Comments")]
        [Required]
        public string Comments { get; set; }

        public string CommentDate { get; set; }

        public string PrevComment { get; set; }

        public string ParentType { get; set; }


        [Display(Name = "Admin Remarks")]
        public string AdminRemarks { get; set; }

        [Display(Name = "Event Status")]
        public int EventStatus { get; set; }

        
        [Display(Name = "FollowUp Date")]
        public string FollowUpDate { get; set; }


         
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

        //--Updateflag indicating that  Update called from Commercial Info on save click
        public int Updateflag { get; set; }

        public List<SelectListItem> OrderList { get; set; }
    }
}