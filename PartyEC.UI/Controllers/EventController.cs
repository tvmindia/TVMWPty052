using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using AutoMapper;
using Newtonsoft.Json;
using PartyEC.UI.Models;

namespace PartyEC.UI.Controllers
{
    public class EventController : Controller
    {
        #region Constructor_Injection

        IEventBusiness _eventBusiness;
        ICategoriesBusiness _categoryBusiness;
        ICommonBusiness _commonBusiness;

        public EventController(IEventBusiness eventBusiness, ICommonBusiness commonBusiness,ICategoriesBusiness categoryBusiness)
        {    
            _eventBusiness = eventBusiness;            
            _commonBusiness = commonBusiness;
            _categoryBusiness = categoryBusiness;
        }

        #endregion Constructor_Injection

        #region Index
        // GET: Event
        public ActionResult Index()
        {
            EventViewModel catobj = new EventViewModel();
            catobj.CategoryList = Mapper.Map<List<Categories>, List<CategoriesViewModel>>(_categoryBusiness.GetAllCategory());
            return View(catobj);       
        }
        #endregion Index

        #region GetAllEvents
        [HttpGet]
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
        public string InsertUpdateEvent(EventViewModel EventObj)
        {
            if (ModelState.IsValid)
            {
                OperationsStatusViewModel OperationsStatusViewModelObj = null;
                if (EventObj.ID == 0)
                { 
                    try
                    {
                        EventObj.commonObj = new CommonViewModel();
                        EventObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                        EventObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.InsertEvent(Mapper.Map<EventViewModel, Event>(EventObj)));                      
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
                        EventObj.commonObj = new CommonViewModel();
                        EventObj.commonObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                        EventObj.commonObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();
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

        #region ChangeButtonStyle
        [HttpGet]
        public ActionResult ChangeButtonStyle(string ActionType)
        {
            ToolboxViewModel ToolboxViewModelObj = new ToolboxViewModel();
            switch (ActionType)
            {
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
    }
}