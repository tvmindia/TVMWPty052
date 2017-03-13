using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyEC.BusinessServices.Contracts;

namespace PartyEC.UI.Controllers
{
    public class CategoriesController : Controller
    {

        #region Constructor_Injection

        ICategoriesBusiness _categoryBusiness;
        ICommonBusiness _commonBusiness;

        public CategoriesController(ICategoriesBusiness categoryBusiness, ICommonBusiness commonBusiness)
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