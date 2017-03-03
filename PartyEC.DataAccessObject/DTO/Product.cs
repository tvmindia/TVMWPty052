using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortDescription {get;set;}
        public char ProductType { get; set; }

        public List<ProductDetail> ProductDetail { get; set; }
        public LogDetails LogDetails { get; set; }       

         
    }

    public class ProductDetail {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public int OutOfStockAlertQty { get; set; }
        public int PriceDifference { get; set; }        
        public string DetailTags { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean StockAvailable { get; set; }
        public Boolean DefaultOption { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime DiscountStartDate{ get; set; }
        public DateTime DiscountEndDate { get; set; }
        public List<ProductAttributeValues> ProductAttributes { get; set; }
    }

    public class ProductAttributeValues
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }

    }
}