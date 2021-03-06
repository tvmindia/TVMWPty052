﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class ShoppingCartViewModel
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
        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public bool FreeDeliveryYN { get; set; }
        public bool StockAvailableYN { get; set; }
        public bool IsPriceChanged { get; set; }
        public string PriceChangedText { get; set; }

        public string ItemStatus { get; set; }
        public string CreatedDate { get; set; }

        public decimal ShippingCharge { get; set; }
        public string ImageURL { get; set; }
        public LogDetails logDetails { get; set; }
        public List<AttributeValues> AttributeValues { get; set; }

    }
    public class WishlistViewModel
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
        public decimal Price { get; set; }
        public string StickerURL { get; set; }
        public LogDetails logDetails { get; set; }
        public List<AttributeValues> AttributeValues { get; set; }
    }
    public class Cart_WishlistAppViewModel
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