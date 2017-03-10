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
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
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
        public string PostTreeOrder(AttributeSetViewModel attributeSetViewModelObj)
        {
            try
            {
                attributeSetViewModelObj.commonObj = new CommonViewModel();
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
                //Adding Created date and Createdby 
                foreach (var i in TreeViewOrder)
                {
                    i.commonObj = attributeSetViewModelObj.commonObj;
                }

                OperationsStatusViewModelObj= Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeToSetLinks.TreeViewUpdateAttributeSetLink((Mapper.Map<List<AttributeSetLinkViewModel>, List<AttributeSetLink>>(TreeViewOrder)), attributeSetViewModelObj.ID));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            if (ActionType == "Edit")
            {
                ToolboxViewModelObj.deletebtn.Visible = true;
                ToolboxViewModelObj.savebtn.Visible = true;
                ToolboxViewModelObj.savebtn.Event = "MainClick()";
            }
            else if (ActionType == "Add")
            {
                ToolboxViewModelObj.deletebtn.Visible = true;
                ToolboxViewModelObj.deletebtn.Disable = true;
                ToolboxViewModelObj.savebtn.Visible = true;
                ToolboxViewModelObj.savebtn.Event = "MainClick()";
            }

            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }
    }
}