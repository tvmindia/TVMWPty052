using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    public class DynamicUIController : Controller
    {
        private IDynamicUIBusiness _dynamicUIBusiness;
        public DynamicUIController(IDynamicUIBusiness dynamicUIBusiness)
        {
            _dynamicUIBusiness = dynamicUIBusiness;
        }


        // GET: DynamicUI
        public ActionResult _MenuNavBar()
        {
            List<Menu> menulist=_dynamicUIBusiness.GetAllMenues();
            DynamicUIViewModel dUIObj = new DynamicUIViewModel();
            dUIObj.MenuViewModelList = Mapper.Map<List<Menu>, List<MenuViewModel>>(menulist);
            return View(dUIObj);
        }
        [HttpGet]
        public string GetTreeListForAttributeSet()
        {
            try
            {
                List<JsTreeNode> NodeList = _dynamicUIBusiness.GetTreeList();
                return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
            }
            catch (Exception ex)
            {
                 return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            //return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
        }
    }
}