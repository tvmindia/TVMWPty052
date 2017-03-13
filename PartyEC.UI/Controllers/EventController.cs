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
        ICommonBusiness _commonBusiness;

        public EventController(IEventBusiness eventBusiness, ICommonBusiness commonBusiness)
        {    
            _eventBusiness = eventBusiness;
            _commonBusiness = commonBusiness;
        }
        #endregion Constructor_Injection


        #region Index
        // GET: Event
        public ActionResult Index()
        {
            EventViewModel catobj = new EventViewModel();
            catobj.CategoryList = new List<CategoriesViewModel>
            {
                 new CategoriesViewModel{ID = 1, Name = "Category1"},
                 new CategoriesViewModel{ID = 2, Name = "Category2"},
                 new CategoriesViewModel{ID = 3, Name = "Category3"},
                 new CategoriesViewModel{ID = 4, Name = "Category4"},
                 new CategoriesViewModel{ID = 5, Name = "Category5"},
                 new CategoriesViewModel{ID = 6, Name = "Category6"}, 
            };
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
                if (EventObj.ID == 0)
                { 
                    try
                    {
                        EventObj.commonObj = new CommonViewModel();
                        EventObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                        EventObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.InsertEvent(Mapper.Map<EventViewModel, Event>(EventObj)));
                        return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
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
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.UpdateEvent(Mapper.Map<EventViewModel, Event>(EventObj)));
                        return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                    }
                    catch (Exception ex)
                    {
                        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                    }
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

        #endregion InsertUpdateEvent

        #region DeleteEvent


        [HttpPost]
        public string DeleteEvent([Bind(Exclude = "Name,RelatedCategories,")] AttributesViewModel attributesObj)
        {
            if (!ModelState.IsValid)
            {
                if (attributesObj.ID != 0)
                {
                    try
                    {
                        OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                        OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_eventBusiness.DeleteEvent(attributesObj.ID, Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                        return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
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
    }
}