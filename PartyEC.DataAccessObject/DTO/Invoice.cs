using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Invoice
    {
        public int ID { get; set; }
        public string InvoiceNo { get; set; }
        public int ParentID { get; set; }
        public string ParentType { get; set; }
        public string InvoiceDate { get; set; }
        public DateTime InvoiceDateTime { get; set; }
        public int PaymentStatus { get; set; }
        public LogDetails log { get; set; }
    }
    public class InvoiceDetail
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int OrderItemID { get; set; }
        public float InvoiceAmt { get; set; }
        public LogDetails log { get; set; }
    }
}