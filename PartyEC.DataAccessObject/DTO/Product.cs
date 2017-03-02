using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortDescription {get;set;}
        public char ProductType { get; set; }

        public Common commonObj { get; set; }

        public OperationsStatus operationsStatusObj { get; set; }
    }
}