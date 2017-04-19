using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Models
{
    public class QuotationsViewModel
    {

        public int ID { get; set; }

        [Display(Name = "Quotation No")]
        public string QuotationNo { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string ProductName { get; set; }


        [Display(Name = "Required Date")]
        public string RequiredDate { get; set; }
        [Display(Name = "Quotation Date")]
        public string QuotationDate { get; set; }
        [Display(Name = "Quotation Status")]
        public string Status { get; set; }
        public string StatusText { get; set; }
        [Display(Name = "Qty")]
        public int Qty { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Additional Charges")]
        public decimal AdditionalCharges { get; set; }
        [Display(Name = "Tax Amount")]
        public decimal TaxAmt { get; set; }
        [Display(Name = "Discount")]
        public decimal DiscountAmt { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }

        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal SubTotal { get; set; }

        public string ImageUrl { get; set; }
        public string SourceIP { get; set; }
        public string ProductSpecXML { get; set; }

        //Customer Details
        public CustomerViewModel customerObj { get; set; }
        //Log Details
        public LogDetailsViewModel commonObj { get; set; }
        //Comments
        public EventsLogViewModel EventsLogViewObj { get; set; }
        //Mailing Comments
        public MailViewModel mailViewModelObj { get; set; }

        public List<SelectListItem> QuotationstatusList { get; set; }

        public List<AttributeValuesViewModel> AttributeValues { get; set; }


    }
}