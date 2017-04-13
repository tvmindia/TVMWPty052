using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Quotations
    {
        public int ID { get; set; }
        public string QuotationNo { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string RequiredDate { get; set; }
        public string QuotationDate { get; set; }
        public string Status { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal DiscountAmt { get; set; }
        public string Message { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal SubTotal { get; set; }
        public string ImageUrl { get; set; }
        public string SourceIP { get; set; }
        public string ProductSpecXML { get; set; }

        //Customer Details
        public Customer customerObj { get; set; }
        //Log Details
        public LogDetails logDetails { get; set; } 
        //Comments
        public EventsLog  EventsLogObj { get; set; }
    }
}