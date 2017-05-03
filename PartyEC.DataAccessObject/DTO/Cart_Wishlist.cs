using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string ProductSpecXML { get; set; }
        public List<AttributeValues> ProductAttributes { get; set; }
        public int Qty { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal Price { get; set; }
        public string ItemStatus { get; set; }
        public string CreatedDate { get; set; }

        public int LocationID { get; set; }
        public decimal ShippingCharge { get; set; }
        public string ImageURL { get; set; }
        
        public LogDetails logDetails { get; set; }
        public List<AttributeValues> AttributeValues { get; set; }
        
    }
    public class Wishlist
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string ProductSpecXML { get; set; }
        public string DaysinWL { get; set; }
        public string CreatedDate { get; set; }
        public string ImageURL { get; set; }
        public LogDetails logDetails { get; set; }
        public List<AttributeValues> AttributeValues { get; set; }
    }

}