using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Attributes
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Caption { get; set; }
        public string AttributeType { get; set; }
        public string CSValues { get; set; }
        public string EntityType { get; set; }

        public bool ConfigurableYN { get; set; }
        public bool FilterYN { get; set; }      
        public bool MandatoryYN { get; set; }
        public bool ComparableYN { get; set; }

        public LogDetails commonObj { get; set; }

        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }
    public class AttributeSet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public AttributeSetLink attributeSetLinkObj;
        public LogDetails commonObj { get; set; }
        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }

    public class AttributeSetLink
    {
        public int ID { get; set; }
        public int AttributeSetID { get; set; }
        public int AttributeID { get; set; }
        public float DisplayOrder { get; set;}
        public LogDetails commonObj { get; set; }

        public OperationsStatus operationsStatusObj { get; set; } // For Insert,Update,Delete
    }


    public class AttributeValues
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Value { get; set; }
        public string DataType { get; set; }
        public bool Isconfigurable { get; set; }//false
       public AttributeValues()
        {
        }

        public AttributeValues( AttributeValues copy) {
            Name = copy.Name;
            Caption = copy.Caption;
            Value = copy.Value;
            DataType = copy.DataType;
            Isconfigurable = copy.Isconfigurable;
        }

    }

}