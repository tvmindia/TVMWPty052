using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Treeview
    {
        public string ID
        {
            get;
            set;
        }
        public int ParentID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string level
        {
            get;
            set;
        }
        public string icon
        {
            get;
            set;
        }
    }
    public class JsTreeNode
    {
        public JsTreeNode()
        {
        }
        public string text
        {
            get;
            set;
        }
        public string id
        {
            get;
            set;
        }
        public string icon
        {
            get;
            set;
        }

        public List<JsTreeNode> children
        {
            get;
            set;
        }

    }
}