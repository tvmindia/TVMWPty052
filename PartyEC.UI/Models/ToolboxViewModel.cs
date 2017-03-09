﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class ToolboxViewModel
    {
        public ToolBoxStructure addbtn;
        public ToolBoxStructure editbtn;
        public ToolBoxStructure deletebtn;
        public ToolBoxStructure savebtn;
        public ToolBoxStructure resetbtn;
        public ToolBoxStructure backbtn;
    }
    public struct ToolBoxStructure
    {
        public string Event { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public bool Disable { get; set; }
    }


}