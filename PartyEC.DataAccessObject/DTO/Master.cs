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
    }

}