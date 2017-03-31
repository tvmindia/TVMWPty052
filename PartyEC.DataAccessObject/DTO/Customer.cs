using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Customer
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Language { get; set; }
        public string Gender { get; set; }
        public Guid? ProfileImageID { get; set; }
        public int? OrdersCount { get; set; }
        public int? BookingsCount { get; set; }
        public int? QuotationsCount { get; set; }
        public int? OrdersCountHistory { get; set; }
        public int? BookingsCountHistory { get; set; }
        public int? QuotationsCountHistory { get; set; }

        public LogDetails logDetailsObj { get; set; }

        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }
}