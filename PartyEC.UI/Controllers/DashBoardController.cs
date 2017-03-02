using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartyEC.UI.Controllers
{
    public class DashBoardController : Controller
    {

        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }
    }
}