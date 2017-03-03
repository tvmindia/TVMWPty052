using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.UI.Models
{
    public class DynamicUIViewModel
    {

       public List<MenuViewModel> MenuViewModelList { get; set; }
       public List<TreeViewModel> TreeViewModelList { get; set; }
    }
}