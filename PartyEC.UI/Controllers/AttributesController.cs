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
    public class AttributesController : Controller
    {
        #region Constructor_Injection
        IAttributesBusiness _attributeBusiness;
        ICommonBusiness _commonBusiness;
        public AttributesController(IAttributesBusiness attributeBusiness, ICommonBusiness commonBusiness)
        {
            _attributeBusiness = attributeBusiness;
            _commonBusiness = commonBusiness;
        } 
        #endregion Constructor_Injection

        // GET: Attributes
        #region Index
        public ActionResult Index()
        {
            AttributesViewModel attributesViewModelObj = new AttributesViewModel();
            return View(attributesViewModelObj);
        }
        #endregion Index

        #region GetAllAttributes
        [HttpGet]
        public string GetAllAttributes(AttributesViewModel attributesObj)
        {
            try
            {
                List<AttributesViewModel> attributeList = Mapper.Map<List<Attributes>, List<AttributesViewModel>>(_attributeBusiness.GetAllAttributes());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attributeList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }            
        }

        #endregion  GetAllAttributes

        #region GetAttributesByID

        [HttpGet]
        public string GetAttributes(string ID)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                AttributesViewModel attribute = Mapper.Map<Attributes,AttributesViewModel>(_attributeBusiness.GetAttributes(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attribute });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetAttributesByID

        #region InsertUpdateAttributes

        [HttpPost]
        public string InsertUpdateAttributes(AttributesViewModel attributesObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                if (attributesObj.ID == 0) //Create Attribute
                {
                    try
                    {
                        attributesObj.commonObj = new LogDetailsViewModel();
                        attributesObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                        attributesObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeBusiness.InsertAttributes(Mapper.Map<AttributesViewModel, Attributes>(attributesObj)));
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
                else //Update Attribute
                {
                    try
                    {
                        attributesObj.commonObj = new LogDetailsViewModel();
                        attributesObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                        attributesObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeBusiness.UpdateAttributes(Mapper.Map<AttributesViewModel, Attributes>(attributesObj)));                        
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
                if (OperationsStatusViewModelObj.StatusCode == 1)
                {
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #endregion InsertUpdateAttributes

        #region DeleteAttributes


        [HttpPost]
        public string DeleteAttributes([Bind(Exclude = "Name,Caption,AttributeType,CSValues,EntityType,ConfigurableYN,FilterYN,MandatoryYN,ComparableYN")] AttributesViewModel attributesObj)
        {
            if (!ModelState.IsValid)
            {
                if (attributesObj.ID != 0)
                {
                    try
                    {
                        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_attributeBusiness.DeleteAttributes(attributesObj.ID));
                        if (OperationsStatusViewModelObj.StatusCode == 1)
                        {
                            return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                        }
                        else
                        {
                            return JsonConvert.SerializeObject(new { Result = "Error", Record = OperationsStatusViewModelObj });
                        }
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }              
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Select attribute" });
        }

        #endregion DeleteAttributes
        
        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "AttributeList":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Event = "btnAddNew()";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    break;
                case "AddNew":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goBack()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "clickSave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Event = "btnReset()";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    break;
                case "EditDetails":
                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goBack()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Event = "clickDelete()";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "clickSave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Event = "btnReset()";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }
        #endregion ChangeButtonStyle


        #region OtherAttributeTemplate
        [HttpGet]
        public ActionResult EditTemplateForAttributes(string setID,string Isconfigurable)
        {
            List<AttributesViewModel> attributesList = null;
            try
            {
               
                if ((!string.IsNullOrEmpty(setID))&&(!string.IsNullOrEmpty(Isconfigurable)))
                {
                    attributesList = Mapper.Map<List<Attributes>, List<AttributesViewModel>>(_attributeBusiness.GetAllAttributeBySet(int.Parse(setID),bool.Parse(Isconfigurable)));
                    //Renders corresponding partial view
                    switch(bool.Parse(Isconfigurable))
                    {
                        case true:
                            return View("_EditTemplateForAssociatedProductAttributes", attributesList);
                        case false:
                            return View("_EditTemplateForAttributes", attributesList);
                    }
                }
              
           
            }
            catch(Exception ex)
            {
                
            }

            return View("", attributesList);
        }
        #endregion OtherAttributeTemplate


        [HttpGet]
        public void SendMessage(Int64 id)
        {
            try
            {
                //if (id < 100000000)
                //    id = 9447217751;
                //_commonBusiness.SendOTP("7751", id.ToString());
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}