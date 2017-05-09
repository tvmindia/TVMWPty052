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
        [Display(Name = "Order No :")]
        public string OrderNo { get; set; }
        [Display(Name = "Revision No :")]
        public int RevNo { get; set; }
        //
        public string RevisionIDs { get; set; }
        
        public string OrderRev { get; set; }
        //
        [Display(Name = "Date :")]
        public string OrderDate { get; set; }
        [Display(Name = "Status :")]
        public string OrderStatus { get; set; }
        //
        [Display(Name = "Name :")]
        public string CustomerName { get; set; }
        [Display(Name = "Mobile :")]
        public string ContactNo { get; set; }
        [Display(Name = "Email :")]
        public string CustomerEmail { get; set; }
        public string CustomerURL { get; set; }
        public string ProfileImageID { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> PaymentStatusList { get; set; }
        //
        public int ParentOrderID { get; set; }
        [Display(Name = "Source IP :")]
        public string SourceIP { get; set; }
	 public int CustomerID { get; set; }
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
        public int ShippedQty { get; set; }
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
        public string ItemStatus { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float ShippingAmt { get; set; }
        public float TaxAmt { get; set; }
        public float DiscountAmt { get; set; }
        public float Total { get; set; }
        public float SubTotal { get; set; }

        //Order Status
        public int Code { get; set; }
        public string Description { get; set; }

        //Sales Statistics
        public string LifeTimeSales { get; set; }
        public string AverageSales { get; set; }
        public string LastMonthSales { get; set; }

        //Comments
        public EventsLogViewModel EventsLogViewObj { get; set; }

        //OrderSummery
        public string SubTotalOrderSummery { get; set; }
        public string TaxAmtOrderSummery { get; set; }
        public string ShippingCostOrderSummery { get; set; }
        public string DiscountAmtOrderSummery { get; set; }
        public string GrandTotalOrderSummery { get; set; }

        //Mailing Comments
        public MailViewModel mailViewModelObj { get; set; }
        public ShipmentViewModel shipmentViewModelObj { get; set; }
    }

    //app view model orders
    public class OrderAppViewModel
    {

        public int ID { get; set; } 
        public string OrderNo { get; set; }
        public int RevNo { get; set; } 
        public string OrderRev { get; set; } 
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerURL { get; set; }
        public string ProfileImageID { get; set; }
        public List<SelectListItem> Countries { get; set; }

 
        //
        public int ParentOrderID { get; set; }
        public string SourceIP { get; set; }
        public int CustomerID { get; set; }
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


        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
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

        //Order Status
        public int Code { get; set; }
        public string Description { get; set; }
           
        //Comments
        public EventsLogViewModel EventsLogViewObj { get; set; }

        //OrderSummery
        public string SubTotalOrderSummery { get; set; }
        public string TaxAmtOrderSummery { get; set; }
        public string ShippingCostOrderSummery { get; set; }
        public string DiscountAmtOrderSummery { get; set; }
        public string GrandTotalOrderSummery { get; set; }

        //Mailing Comments
        public MailViewModel mailViewModelObj { get; set; }

        public List<AttributeValuesViewModel> AttributeValues { get; set; }
    }
    public class OrderDetailViewModel
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public int ProductQty { get; set; }
        public string ProductSpecXML { get; set; }
        public string ItemStatus { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float ShippingAmt { get; set; }
        public float TaxAmt { get; set; }
        public float DiscountAmt { get; set; }
        public float TotalDiscountAmt { get; set; }
        public float Total { get; set; }
        public float SubTotal { get; set; }
        public List<OrderDetailViewModel> OrderDetailsList { get; set; }
        public LogDetailsViewModel commonObj { get; set; }
        public int ShippedQty { get; set; }
        public int QtyShipped { get; set; }
    }
}