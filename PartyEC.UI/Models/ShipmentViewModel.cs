using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class ShipmentViewModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string ShipmentNo { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShipmentDateString { get; set; }
        [Display(Name = "Delivered Date :")]
        public string DeliveredDate { get; set; }
        [Display(Name = "Delivered By :")]
        public string DeliveredBy { get; set; }
        public LogDetailsViewModel log { get; set; }
        public List<ShipmentDetailViewModel> DetailsList { get; set; }
    }
    public class ShipmentDetailViewModel
    {
        public int ID { get; set; }
        public int ShipmentID { get; set; }
        public int OrderItemID { get; set; }
        public int ShippedQty { get; set; }
        public LogDetailsViewModel log { get; set; }
        public OrderDetailViewModel OrderDetailObj { get; set; }
    }
}