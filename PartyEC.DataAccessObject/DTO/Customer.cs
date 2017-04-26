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
        public string Address { get; set; }
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
        public bool IsActive { get; set; }
        public int CartCount { get; set; }
        public int WishCount { get; set; }
        public CustomerAddress customerAddress { get; set; }
        public LogDetails logDetailsObj { get; set; }

        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }
    public class CustomerAddress
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public Country country { get; set; }
        public string StateProvince { get; set; }
        public string ContactNo { get; set; }
        public bool BillDefaultYN { get; set; }
        public bool ShipDefaultYN { get; set; }

        public LogDetails logDetailsObj { get; set; }



    }
}