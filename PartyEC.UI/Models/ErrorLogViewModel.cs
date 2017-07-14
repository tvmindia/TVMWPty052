using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class ErrorLogViewModel
    {
    }
    public class ErrorLogAppViewModel
    {
        public string REPORT_ID { get; set; }
        public string PACKAGE_NAME { get; set; }
        public Object BUILD { get; set; }
        public string LOGCAT { get; set; }
        public string ANDROID_VERSION { get; set; }
        public string APP_VERSION_CODE { get; set; }
        public string AVAILABLE_MEM_SIZE { get; set; }
        public Object CRASH_CONFIGURATION { get; set; }
    }
}