﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class OperationsStatusViewModel
    {
        public Int16 StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object ReturnValues { get; set; }
    }
}