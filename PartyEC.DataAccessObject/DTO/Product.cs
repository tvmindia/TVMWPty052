using System;
using System.Collections.Generic;

namespace PartyEC.DataAccessObject.DTO
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public Boolean Enabled { get; set; }
        public string Unit { get; set; }      
        public string URL { get; set; } //partyec web url for this product,share option       
        public char ActionType { get; set; }//book //buy //Quote
        public char ProductType { get; set; }//simple //configurable
        public int AttributeSetID { get; set; }

        public int SupplierID { get; set; }
        public int ManufacturerID { get; set; }
        public string SupplierName { get; set; }
        public string ManufacturerName { get; set; }

        public string TaxClass { get; set; }
        public decimal BaseSellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public Boolean ShowPrice { get; set; }

        public string ShortDescription {get;set;}
        public string LongDescription { get; set; }
        public Boolean StockAvailable { get; set; }
        public Boolean FreeDelivery { get; set; }

        public string HeaderTags { get; set; }
        public Guid StickerID { get; set; }
        public string StickerURL { get; set; }
        public bool IsCategorized { get; set; }
        public int CategoryID { get; set; }
        public float PositionNo { get; set; }
        public List<ProductDetail> ProductDetail { get; set; }
        public ProductDetail productDetail { get; set; }
        public LogDetails logDetails { get; set; }
        public List<ProductImages>ProductHeaderImages { get; set; }
        public Product() {
            //LogDetails = new LogDetails();
           // ProductDetail = new List<ProductDetail>();
            ProductHeaderImages = new List<ProductImages>();
        }
    }

    public class ProductDetail {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int AttributeSetID { get; set; }
        public int Qty { get; set; }
        public int OutOfStockAlertQty { get; set; }
        public decimal PriceDifference { get; set; }        
        public string DetailTags { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean StockAvailable { get; set; }
        public Boolean DefaultOption { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime DiscountStartDate{ get; set; }
        public DateTime DiscountEndDate { get; set; }
        public LogDetails logDetails { get; set; }
        public List<AttributeValues> ProductAttributes { get; set; }
        public List<ProductImages> ProductDetailImages { get; set; }

        public ProductDetail() {
            //logDetails = new LogDetails();
            ProductAttributes = new List<AttributeValues>();
            ProductDetailImages = new List<ProductImages>();
        }

        public struct ProductTypes
        {
            public const string Simple = "s";
            public const string Configurable = "c";

        }

        public struct ActionTypes
        {
            public const string Book = "b";
            public const string Buy = "p";
            public const string Quote = "q";

        }
    }

   

    public class ProductImages {
        public int ID { get; set; }
        public string URL { get; set; }
        public bool isMain { get; set; }
    }

    
}