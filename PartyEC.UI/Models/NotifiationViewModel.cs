using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class NotifiationViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public CustomerViewModel customer { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public Int16 Status { get; set; }
        public LogDetailsViewModel logDetailsObj { get; set; }

    }
}