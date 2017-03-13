using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyEC.BusinessServices.Contracts;

namespace PartyEC.UI.Controllers
{
    public class CategoryController : Controller
    {

        #region Constructor_Injection

        ICategoryBusiness _categoryBusiness;
        ICommonBusiness _commonBusiness;

        public CategoryController(ICategoryBusiness categoryBusiness, ICommonBusiness commonBusiness)
        {
            _categoryBusiness = categoryBusiness;
            _commonBusiness = commonBusiness;
        }
        #endregion Constructor_Injection
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}