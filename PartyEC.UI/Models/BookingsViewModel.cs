using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Models
{
    public class BookingsViewModel
    {
        public int ID { get; set; }
        public string BookingNo { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CustomerID { get; set; }
        public string RequiredDate { get; set; }
        public string BookingDate { get; set; }
        public string SourceIP { get; set; }
        public int Status { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal SubTotal { get; set; }
        public string Message { get; set; }
        public string BillPrefix { get; set; }
        public string BillFirstName { get; set; }
        public string BillMidName { get; set; }
        public string BillLastName { get; set; }
        public string BillAddress { get; set; }
        public string BillCity { get; set; }
        public string BillCountryCode { get; set; }
        public string BillStateProvince { get; set; }
        public string BillContactNo { get; set; }

        public decimal Total { get; set; } 
        public string StatusText { get; set; }
        public string ImageUrl { get; set; }
        public string ProductSpecXML { get; set; }

        //Customer Details
        public CustomerViewModel customerObj { get; set; }
        //Log Details
        public LogDetailsViewModel logDetails { get; set; }
        //Comments
        public EventsLogViewModel EventsLogViewObj { get; set; }
        //Mailing Comments
        public MailViewModel mailViewModelObj { get; set; }
        public List<AttributeValuesViewModel> AttributeValues { get; set; }
     
        public List<SelectListItem> BookingsstatusList { get; set; }
    }
    public class BookingsAppViewModel
    {
        public int ID { get; set; }
        public string BookingNo { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string RequiredDate { get; set; }
        public string BookingDate { get; set; }
        public string SourceIP { get; set; }
        public int Status { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal DiscountAmt { get; set; }
        public string Message { get; set; }
        public string BillPrefix { get; set; }
        public string BillFirstName { get; set; }
        public string BillMidName { get; set; }
        public string BillLastName { get; set; }
        public string BillAddress { get; set; }
        public string BillCity { get; set; }
        public string BillCountryCode { get; set; }
        public string BillStateProvince { get; set; }
        public string BillContactNo { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }

        //Customer Details
        public CustomerViewModel customerObj { get; set; }
        //Comments
        public EventsLogViewModel EventsLogViewObj { get; set; }

    }

}