using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Categories
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Ischecked { get; set; }
        public int ParentID { get; set; }
        public bool System { get; set; }
        public int ChildrenCount { get; set; }
        public bool Filter { get; set; }
        public bool Navigation { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }
        public float CategoryOrder { get; set; }
        public string ImageID { get; set; }
        public LogDetails commonObj { get; set; }
        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }
}