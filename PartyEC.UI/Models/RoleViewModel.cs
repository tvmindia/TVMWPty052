using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class RoleViewModel
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public LogDetailsViewModel logDetails { get; set; }
    }
}