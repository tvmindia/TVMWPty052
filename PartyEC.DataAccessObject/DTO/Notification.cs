using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Notification
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public Customer customer { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Int16 Status { get; set; }
        public LogDetails logDetailsObj { get; set; }
    }
}