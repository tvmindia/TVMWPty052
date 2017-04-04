using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Cart_Wishlist
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string ProductSpecXML { get; set; }
        public int Qty { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal Price { get; set; }
        public string ItemStatus { get; set; }
        public string CreatedDate { get; set; }

        public int CartCount { get; set; }
        public int WishCount { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string DaysinWL { get; set; }               
        public List<AttributeValues> AttributeValues { get; set; }
        
    }
}