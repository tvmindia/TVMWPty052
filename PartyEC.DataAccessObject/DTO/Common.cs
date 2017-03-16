using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.DataAccessObject.DTO
{
    public class LogDetails
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }       
       
    }

    public class OtherImages {

        public Guid ID { get; set; }
        public string ImageType { get; set; }
        public string URL { get; set; }
        public LogDetails LogDetails { get; set; }

        public struct ImageTypes
        {
            public const string Sticker = "Sticker";
            public const string ProfileImage = "Profile";
            public const string CategoryImage = "Category";
            public const string EventTypeImage = "EventType";

        }
    }

    public class Const
    {
        #region Messages

        public string InsertFailure
        {
            get { return "Insertion Not Successfull! "; }
        }

        public string InsertSuccess
        {
            get { return "Insertion Successfull! "; }
        }

        public string  UpdateFailure
        {
            get { return "Updation Not Successfull! "; }
        }

        public string UpdateSuccess
        {
            get { return "Updation Successfull! "; }
        }

        public string DeleteFailure
        {
            get { return "Deletion Not Successfull! "; }
        }
        public string DeleteSuccess
        {
            get { return "Deletion Successfull! "; }
        }
        public string FKviolation
        {
            get { return "Foreign key violation-Deletion Not Successfull!"; }
        }
        #endregion Messages

    }
}