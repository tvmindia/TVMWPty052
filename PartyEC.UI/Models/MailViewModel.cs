﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class MailViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string OrderComment { get; set; }
        public int OrderNo { get; set; }
        public int OrderID { get; set; }
        public string TemplateString { get; set; }
        public string MailSubject { get; set; }
    }
}