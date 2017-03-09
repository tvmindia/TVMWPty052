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
    public class AttributeSetLinkController : Controller
    {
        IAttributeToSetLinks _attributeToSetLinks;
        ICommonBusiness _commonBusiness;
        public AttributeSetLinkController(IAttributeToSetLinks attributeToSetLinks, ICommonBusiness commonBusiness)
        {
            _attributeToSetLinks = attributeToSetLinks;
            _commonBusiness = commonBusiness;
        }
        // GET: AttributeSetLink
        public ActionResult Index()
        {
            return View();
        }
       
    }
}