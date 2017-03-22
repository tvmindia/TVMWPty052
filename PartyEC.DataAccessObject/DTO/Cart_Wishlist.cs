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
        public string ProductSpecXML { get; set; }
        public int Qty { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal Price { get; set; }
        public int ItemStatus { get; set; }
    }
}