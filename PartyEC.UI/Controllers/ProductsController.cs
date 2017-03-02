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
    public class ProductsController : Controller
    {

        IProductBusiness _productBusiness;
        public ProductsController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public string GetAllProductss(ProductViewModel productObj)
        {
            try
            {
             //   _productBusiness.GetAllProducts(Mapper.Map<ProductViewModel, Product>(productObj);
             //   Mapper.Map<Product, ProductViewModel>(_productBusiness.GetAllProducts(Mapper.Map<ProductViewModel, Product>(productObj)));
            //   return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
            }
            catch (Exception)
            {

            }
            return JsonConvert.SerializeObject(new { Result = "OK", Record = "List of objects" });
        }

        [HttpPost]
        public string ProductInsert(ProductViewModel productObj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    productObj.commonObj = new CommonViewModel();
                    productObj.commonObj.CreatedBy = "Albert Thomson";
                    OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_productBusiness.InsertProduct(Mapper.Map<ProductViewModel, Product>(productObj)));
                    return JsonConvert.SerializeObject(new { Result = "OK", Record = OperationsStatusViewModelObj });
                }
                catch (Exception ex)
                {
                    return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
                }
            }
            return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "Please Check the values" });
        }

      
      
    }
}