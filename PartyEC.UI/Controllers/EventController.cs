﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using AutoMapper;
using Newtonsoft.Json;
using PartyEC.UI.Models;
using PartyEC.UI.CustomAttributes;

namespace PartyEC.UI.Controllers
{
    [CustomAuthenticationFilter]
    public class EventController : Controller
    {
       
        #region Constructor_Injection

        IEventBusiness _eventBusiness;
        ICategoriesBusiness _categoryBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;

        public EventController(IEventBusiness eventBusiness, ICommonBusiness commonBusiness,ICategoriesBusiness categoryBusiness,IMasterBusiness masterBusiness)
        {    
            _eventBusiness = eventBusiness;            
            _commonBusiness = commonBusiness;
            _categoryBusiness = categoryBusiness;
            _masterBusiness = masterBusiness;
        }

        #endregion Constructor_Injection

        #region Index
        // GET: Event
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Index()
        {
            EventViewModel catobj = new EventViewModel();
            catobj.CategoryList = Mapper.Map<List<Categories>, List<CategoriesViewModel>>(_categoryBusiness.GetAllCategory());
            return View(catobj);       
        }
        #endregion Index

        #region GetAllEvents
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetAllEvents(EventViewModel eventObj)
        {
            try
            {
                List<EventViewModel> eventList = Mapper.Map<List<Event>, List<EventViewModel>>(_eventBusiness.GetAllEvents());
                return JsonConvert.SerializeObject(new { Result = "OK", Records = eventList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }
        #endregion  GetAllEvents

        #region GetEventByID

        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string GetEvent(string ID)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                EventViewModel attribute = Mapper.Map<Event, EventViewModel>(_eventBusiness.GetEvent(Int32.Parse(ID), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Records = attribute });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }


        #endregion GetEventByID

        #region InsertUpdateEvent

        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string InsertUpdateEvent(EventViewModel EventObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                if (EventObj.ID == 0)
                { 
                    try
                    {
                        EventObj.commonObj = new LogDetailsViewModel();
                        EventObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                        EventObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.InsertEventTypes(Mapper.Map<EventViewModel, Event>(EventObj)));                      
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
                else
                { 
                    try
                    {
                        EventObj.commonObj = new LogDetailsViewModel();
                        EventObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                        EventObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                        EventObj.EventImageID = null;
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.UpdateEvent(Mapper.Map<EventViewModel, Event>(EventObj)));                       
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

        #endregion InsertUpdateEvent

        #region DeleteEvent


        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string DeleteEvent([Bind(Exclude = "Name,RelatedCategories")] AttributesViewModel attributesObj)
        {
            if (!ModelState.IsValid)
            {
                if (attributesObj.ID != 0)
                {
                    try
                    {
                        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.DeleteEvent(attributesObj.ID));
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

        #endregion DeleteEvent   
        #region DeleteOtherImage

        [HttpPost]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public string DeleteOtherImage(EventViewModel EventObj)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                    OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.DeleteOtherImage(EventObj.EventImageID,EventObj.imageType));
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
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }
        #endregion DeleteOtherImage

        #region ChangeButtonStyle
        [HttpGet]
        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
                case "EventList":
                    ToolboxViewModelObj.addbtn.Visible = true;
                    ToolboxViewModelObj.addbtn.Event = "btnAddNew()";
                    ToolboxViewModelObj.addbtn.Title = "Add New";
                    break;
                case "Edit":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Event = "clickdelete()";
                    ToolboxViewModelObj.deletebtn.Title = "Delete";

                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "clicksave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Event = "btnreset()";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";

                    break;
                case "Add":
                    ToolboxViewModelObj.deletebtn.Visible = true;
                    ToolboxViewModelObj.deletebtn.Disable = true;
                    ToolboxViewModelObj.savebtn.Visible = true;
                    ToolboxViewModelObj.savebtn.Event = "clicksave()";
                    ToolboxViewModelObj.savebtn.Title = "Save";

                    ToolboxViewModelObj.resetbtn.Visible = true;
                    ToolboxViewModelObj.resetbtn.Event = "btnreset()";
                    ToolboxViewModelObj.resetbtn.Title = "Reset";

                    ToolboxViewModelObj.backbtn.Visible = true;
                    ToolboxViewModelObj.backbtn.Event = "goback()";
                    ToolboxViewModelObj.backbtn.Title = "Back";
                    break;
                default:
                    return Content("Nochange");
            }
            return PartialView("_ToolboxView", ToolboxViewModelObj);
        }
        #endregion ChangeButtonStyle

        [AuthorizeRoles(RoleContants.SuperAdminRole, RoleContants.AdministratorRole, RoleContants.ManagerRole)]
        public ActionResult Upload(EventViewModel eventViewObj)
        {
            OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();

            var file = Request.Files["Filedata"];
            var FileNameCustom = Guid.NewGuid() + ".png";
            string savePath = Server.MapPath(@"~\Content\OtherImages\" + FileNameCustom);
            file.SaveAs(savePath);
            eventViewObj.URL = "/Content/OtherImages/" + FileNameCustom;
            //if ((eventViewObj.EventImageID == null) || (eventViewObj.EventImageID == ""))
            //{
                eventViewObj.commonObj = new LogDetailsViewModel();
                eventViewObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                eventViewObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                eventViewObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                eventViewObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
                operationsStatus = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.InsertImageEvents(Mapper.Map<EventViewModel, Event>(eventViewObj)));

            //}
            return Content(Url.Content(@"~\Content\OtherImages\" + FileNameCustom));
        }
    }
}