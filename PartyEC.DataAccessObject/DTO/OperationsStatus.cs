using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class OperationsStatus
    {
        
        public Int16 StatusCode { get; set; }
        public object ReturnValues { get; set; }
        public string StatusMessage { get; set; }
        public Exception Exception { get; set; }
    }
}