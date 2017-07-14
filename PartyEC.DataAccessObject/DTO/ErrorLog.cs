using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class ErrorLog
    {
        public string ErrorID
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Date
        {
            get;
            set;
        }
        public string Module
        {
            get;
            set;
        }
        public string Method
        {
            get;
            set;
        }
        public Boolean IsFixed
        {
            get;
            set;
        }
        public string BugFixDate
        {
            get;
            set;
        }
        public string ErrorSource
        {
            get;
            set;
        }
        public Boolean IsMobile
        {
            get;
            set;
        }
        public string AppBuild
        {
            get;
            set;
        }
        public string AppLogCat
        {
            get;
            set;
        }        
        public string Version
        {
            get;
            set;
        }
        public LogDetails commonObj { get; set; }
        public OperationsStatus operationsStatusObj { get; set; } // For return status after Insert,Update,Delete

    }
}