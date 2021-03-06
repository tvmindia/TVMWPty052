﻿using System;
using System.Collections.Generic;

namespace PartyEC.DataAccessObject.DTO
{
    public class Order
    {
        public int ID { get; set; }
        public string RevisionIDs { get; set; }
        public string OrderNo { get; set; }
        public int RevNo { get; set; }
        //
        public string OrderRev { get; set; }
        //
        public string OrderDate { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int StatusCode { get; set; }
        public string OrderStatus { get; set; }
        public int ParentOrderID { get; set; }
        public string SourceIP { get; set; }
        public int CustomerID { get; set; }
        //
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerURL { get; set; }
        public string ProfileImageID { get; set; }
        //
       
        public int shippingLocationID { get; set; }
        public string ShippingLocationName { get; set; }
        public string PaymentType { get; set; }
        public string CurrencyCode { get; set; }
        public float CurrencyRate { get; set; }
        public float TotalOrderAmt { get; set; }
        public float TotalShippingAmt { get; set; }
        public float TotalTaxAmt { get; set; }
        public float TotalDiscountAmt { get; set; }
        public int PayStatusCode { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderRemarks { get; set; }
        public string BillPrefix { get; set; }
        public string BillFirstName { get; set; }
        public string BillMidName { get; set; }
        public string BillLastName { get; set; }
        public string BillAddress { get; set; }
        public string BillCity { get; set; }
        public string BillCountryCode { get; set; }
        public string BillStateProvince { get; set; }
        public string BillContactNo { get; set; }
        public string ShipPrefix { get; set; }
        public string ShipFirstName { get; set; }
        public string ShipMidName { get; set; }
        public string ShipLastName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountryCode { get; set; }
        public string ShipStateProvince { get; set; }
        public string ShipContactNo { get; set; }
        public LogDetails commonObj { get; set; }
        public List<OrderDetail> OrderDetailsList { get; set; }

        //Order Details
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductSpecXML { get; set; }
        public string ItemStatus { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float ShippingAmt { get; set; }
        public float TaxAmt { get; set; }
        public float DiscountAmt { get; set; }
        
        public float Total { get; set; }
        public float SubTotal { get; set; }
        //Sales Statistics
        public string LifeTimeSales { get; set; }
        public string AverageSales { get; set; }
        public string LastMonthSales { get; set; }
        //Comments
        public EventsLog EventsLogObj { get; set; }

        //OrderSummary
        public string SubTotalOrderSummary { get; set; }
        public string TaxAmtOrderSummary { get; set; }
        public string ShippingCostOrderSummary { get; set; }
        public string DiscountAmtOrderSummary { get; set; }
        public string GrandTotalOrderSummary { get; set; }

        public CustomerAddress CustomerBillAddress { get; set; }
        public CustomerAddress CustomerShippingAddress { get; set; }
    }
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int ProductQty { get; set; }
        public string ProductSpecXML { get; set; }
        public string ProductSpecXML1 { get; set; }
        public string ItemStatus { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal ShippingAmt { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal TotalDiscountAmt { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public List<OrderDetail> OrderDetailsList { get; set; }
        public LogDetails commonObj { get; set; }
        public int ShippedQty { get; set; }
        public int QtyShipped { get; set; }
        public List<AttributeValues> AttributeValues { get; set; }
        public string SupplierName { get; set; }
        public int CartId { get; set; }
    }
}