using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class Mail
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string OrderComment { get; set; }
        public int OrderNo { get; set; }
    }
}