using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Models
{
    public class OrderViewModel
    {

     public int ID{ get; set; }

     public string OrderNo { get; set; }   
     public int RevNo { get; set; }
        //
        public string OrderRev { get; set; }
        //
     public DateTime? OrderDate { get; set; }
     public string OrderStatus { get; set; }
        //
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        //
     public int ParentOrderID { get; set; }
	 public string SourceIP { get; set; }
	 public int CustomerID { get; set; }
	 public int ShippingLocationID { get; set; }
	 public string PaymentType { get; set; }
	 public string CurrencyCode { get; set; }
	 public float CurrencyRate { get; set; }
	 public float TotalOrderAmt { get; set; }
	 public float TotalShippingAmt { get; set; }
	 public float TotalTaxAmt { get; set; }
	 public float TotalDiscountAmt { get; set; }
	 public int PaymentStatus { get; set; }
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
     public LogDetailsViewModel commonObj { get; set; }


        //Order Details
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductSpecXML { get; set; }
        public int ItemStatus { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float ShippingAmt { get; set; }
        public float TaxAmt { get; set; }
        public float DiscountAmt { get; set; }


        //Order Status
        public int Code { get; set; }
        public string Description { get; set; }

        //Sales Statistics
        public string LifeTimeSales { get; set; }
        public string AverageSales { get; set; }
        public string LastMonthSales { get; set; }


    }
}