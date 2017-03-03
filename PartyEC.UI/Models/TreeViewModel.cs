using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class TreeViewModel
    {
        public int ID
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
    }
}