using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class FileUploadViewModel
    {
        public string ImageID { get; set; }
        public HttpPostedFile CategoryImageUpload { get; set; }
    }
}