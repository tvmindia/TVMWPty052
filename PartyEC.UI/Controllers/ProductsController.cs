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
        public string GetAllProducts(ProductViewModel productObj)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>,List<ProductViewModel>>(_productBusiness.GetAllProducts(Mapper.Map<ProductViewModel, Product>(productObj)));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
           // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
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