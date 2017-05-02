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
    public class AttributeSetController : Controller
    {
        IAttributeSetBusiness _attributeSetBusiness;
        IAttributeToSetLinks _attributeToSetLinks;
        ICommonBusiness _commonBusiness;
        public AttributeSetController(IAttributeSetBusiness attributeSetBusiness,IAttributeToSetLinks attributeToSetLinks, ICommonBusiness commonBusiness)
        {
            _attributeSetBusiness = attributeSetBusiness;
            _attributeToSetLinks = attributeToSetLinks;
            _commonBusiness = commonBusiness;
        }
        // GET: AttributeSet
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllAttributeSet()
        {
            try
            {
                List<AttributeSetViewModel> AttributeSetViewList = Mapper.Map<List<AttributeSet>, List<AttributeSetViewModel>>(_attributeSetBusiness.GetAllAttributeSet());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = AttributeSetViewList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string PostTreeOrder(AttributeSetViewModel attributeSetViewModelObj)
        {
            try
            {
                attributeSetViewModelObj.commonObj = new LogDetailsViewModel();
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                //Checking ID empty or not
                if (attributeSetViewModelObj.ID ==0)
                {
                    attributeSetViewModelObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    attributeSetViewModelObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeSetBusiness.InsertAttributeSet((Mapper.Map<AttributeSetViewModel, AttributeSet>(attributeSetViewModelObj))));
                    attributeSetViewModelObj.ID = int.Parse(OperationsStatusViewModelObj.ReturnValues.ToString());
                }
                else
                {
                    attributeSetViewModelObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                    attributeSetViewModelObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                    attributeSetViewModelObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    attributeSetViewModelObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeSetBusiness.UpdateAttributeSet((Mapper.Map<AttributeSetViewModel, AttributeSet>(attributeSetViewModelObj)), attributeSetViewModelObj.ID));
                }
                //Deserialize the string to object
                List<AttributeSetLinkViewModel> TreeViewOrder = JsonConvert.DeserializeObject<List<AttributeSetLinkViewModel>>(attributeSetViewModelObj.TreeList);
                if((TreeViewOrder.Count>3)&&(attributeSetViewModelObj.ID!=0))
                {
                    foreach (var i in TreeViewOrder)
                    {
                        i.commonObj = attributeSetViewModelObj.commonObj;
                    }

                    OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeToSetLinks.TreeViewUpdateAttributeSetLink((Mapper.Map<List<AttributeSetLinkViewModel>, List<AttributeSetLink>>(TreeViewOrder)), attributeSetViewModelObj.ID));
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                }
                //Adding Created date and Createdby 
               else
                {
                    if(attributeSetViewModelObj.ID != 0)
                    {                       
                        return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Records = OperationsStatusViewModelObj });
                    }
                    
                   
                }
               
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string DeleteAttributeSet(AttributeSetViewModel attributeSetObj)
        {
            if (!ModelState.IsValid)
            {
                if (attributeSetObj.ID != 0)
                {
                    try
                    {
                        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeSetBusiness.DeleteAttributeSet(attributeSetObj.ID));
                        return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Select Attribute Set" });
        }
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "Initialize":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Event = "AddNew()";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    break;
                case "Edit":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Event = "Delete()";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "MainClick()";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    break;
                case "Add":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "MainClick()";
                    ToolboxViewModelObj.savebtn.Title = "Save";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }
    }
}