using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RelatedCategoriesCSV { get; set; }
        public Guid EventImageID { get; set; }

        public LogDetails commonObj { get; set; }
        public OperationsStatus operationsStatusObj { get; set; } // For return status after Insert,Update,Delete
    }
}