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
        ICommonBusiness _commonBusiness;
        public AttributeSetController(IAttributeSetBusiness attributeSetBusiness, ICommonBusiness commonBusiness)
        {
            _attributeSetBusiness = attributeSetBusiness;
            _commonBusiness = commonBusiness;
        }
        // GET: AttributeSet
        public ActionResult Index()
        {
            return View();
        }
       

    }
}