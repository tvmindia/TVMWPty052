using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Ischecked { get; set; }

        public LogDetails commonObj { get; set; }
        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }
}