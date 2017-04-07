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
        public string URL { get; set; }
        public LogDetails LogDetails { get; set; }
        public string ImageType { get; set; }
        
    }
    public static class ImageTypesPreffered
    {
        public static readonly string Sticker = "Sticker";
        public static readonly string ProfileImage = "Profile";
        public static readonly string CategoryImage = "Category";
        public static readonly string EventTypeImage = "EventType";

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
            get { return "Deletion Not Successfull!-Already In Use"; }
        }
        public string Duplicate
        {
            get { return "Allready Exist.."; }
        }
        public string NoItems
        {
            get { return "No items"; }
        }
        #endregion Messages

        #region Strings
        public string AppUser
        {
            get { return "App User"; }
        }
        #endregion
    }

    public class FilterCriteria
    {
        public string filterCriteriaCSV { get; set; }
    }
}