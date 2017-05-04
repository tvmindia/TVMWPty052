using System;
using System.Collections.Generic;
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
        public DateTime DeliveredDate { get; set; }
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
    }
}