using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class LogDetailsViewModel    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
      
    }
    public class OtherImagesViewModel
    {

        public Guid ID { get; set; }
        public string ImageType { get; set; }
        public string URL { get; set; }
        public LogDetailsViewModel LogDetails { get; set; }

        public struct ImageTypes
        {
            public const string Sticker = "Sticker";
            public const string ProfileImage = "Profile";
            public const string CategoryImage = "Category";
            public const string EventTypeImage = "EventType";

        }
    }
}