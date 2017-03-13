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
                 new CategoriesViewModel{ID = 1, Name = "Category1", Checked = false},
                 new CategoriesViewModel{ID = 2, Name = "Category2", Checked = false},
                 new CategoriesViewModel{ID = 3, Name = "Category3", Checked = false},
                 new CategoriesViewModel{ID = 4, Name = "Category4", Checked = false},
                 new CategoriesViewModel{ID = 5, Name = "Category5", Checked = false},
                 new CategoriesViewModel{ID = 6, Name = "Category6", Checked = false}, 
            };
            return View(catobj);


          //  return View();
        }
        #endregion Index
    }
}