using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class ToolboxViewModel
    {
      
        public ToolBoxStructure addbtn;
        public ToolBoxStructure addsubbtn;
        public ToolBoxStructure editbtn;
        public ToolBoxStructure deletebtn;
        public ToolBoxStructure savebtn;
        public ToolBoxStructure resetbtn;
        public ToolBoxStructure backbtn;
        public ToolBoxStructure cancelbtn;
        public ToolBoxStructure sendbtn;
        public ToolBoxStructure invoicebtn;
        public ToolBoxStructure shipbtn;
        public ToolBoxStructure actDeactbtn;
        public ToolBoxStructure approve;
        public ToolBoxStructure previewbtn;

    }
    public struct ToolBoxStructure
    {
        public string Event { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public bool Disable { get; set; }
        public bool Pause { get; set; }
    }

    public class ToolBox
    {
        public string Dom { get; set; }
        public string Action { get; set; }
        public string ViewModel { get; set; }
    }


}