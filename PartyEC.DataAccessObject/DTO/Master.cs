using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
   public class Manufacturer
    {
        public Manufacturer()
        {
            country = new Country();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public Country country { get; set; }
    }

    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public LogDetails commonObj { get; set; }
        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }

    public class ShippingLocations
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public LogDetails commonObj { get; set; }
        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }


    public class OrderStatusMaster
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}