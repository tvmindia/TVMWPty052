using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class QuotationsViewModel
    {

        public int ID;
       
        [Display(Name = "Quotation No")]
        public string QuotationReqNo;
        public int ProductID;
        public int CustomerID;
     

        [Display(Name = "Required Date")]
        public string RequiredDate;
        [Display(Name = "Quotation Date")]
        public string QuotationDate;
        [Display(Name = "Quotation Status")]
        public int Status;
        [Display(Name = "Qty")]
        public int Qty;
        [Display(Name = "Price")]
        public decimal Price;
        [Display(Name = "Additional Charges")]
        public decimal AdditionalCharges;
        [Display(Name = "Tax Amount")]
        public decimal TaxAmt;
        [Display(Name = "Discount")]
        public decimal DiscountAmt;
        [Display(Name = "Message")]
        public string Message;

        public decimal GrandTotal;
        public decimal SubTotal;
        
        //Customer Details
        public CustomerViewModel customer { get; set; }
        //Log Details
        public LogDetailsViewModel commonObj { get; set; }
        //Comments
        public EventsLogViewModel EventsLogViewObj { get; set; }


    }
}