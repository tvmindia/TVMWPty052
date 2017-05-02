using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class DynamicUIController : Controller
    {
        private IDynamicUIBusiness _dynamicUIBusiness;
        public DynamicUIController(IDynamicUIBusiness dynamicUIBusiness)
        {
            _dynamicUIBusiness = dynamicUIBusiness;
        }



        // GET: DynamicUI
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult _MenuNavBar()
        {
            List<Menu> menulist = _dynamicUIBusiness.GetAllMenues();
            DynamicUIViewModel dUIObj = new DynamicUIViewModel();
            dUIObj.MenuViewModelList = Mapper.Map<List<Menu>, List<MenuViewModel>>(menulist);
            return View(dUIObj);
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetTreeListForAttributeSet(string ID)
        {
            try
            {
                List<JsTreeNode> NodeList = null;
                if (ID != "")
                {
                    NodeList = _dynamicUIBusiness.GetTreeListAttributeSet(ID);
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
                }
                return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            //return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
        }

        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetTreeListAttributes(string ID)
        {
            try
            {
                List<JsTreeNode> NodeList = _dynamicUIBusiness.GetTreeListAttributes(ID);
                return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetTreeListCategories()
        {
            try
            {
                List<JsTreeNode> NodeList = _dynamicUIBusiness.GetTreeListCategories();
                return JsonConvert.SerializeObject(new { Result = "OK", Records = NodeList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //[HttpGet]
        //public ActionResult ChangeButtonStyle(string ActionType)
        //{
        //    ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
        //    if (ActionType=="Edit")
        //    {
        //        ToolboxViewModelObj.deletebtn.Visible = true;
        //        ToolboxViewModelObj.savebtn.Visible = true;
        //        ToolboxViewModelObj.savebtn.Event = "MainClick()";
        //    }
        //    else if(ActionType == "Add")
        //    {
        //        ToolboxViewModelObj.deletebtn.Visible = true;
        //        ToolboxViewModelObj.deletebtn.Disable = true;
        //        ToolboxViewModelObj.savebtn.Visible = true;
        //        ToolboxViewModelObj.savebtn.Event = "MainClick()";
        //    }

        //     return PartialView("_ToolboxView", ToolboxViewModelObj);
        //}
    }
}