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
        ICommonBusiness _commonBusiness;
        public ProductsController(IProductBusiness productBusiness, ICommonBusiness commonBusiness)
        {
            _productBusiness = productBusiness;
            _commonBusiness = commonBusiness;
        }

        // GET: Products
        public ActionResult Index()
        {
            //suv test
            //OperationsStatus myStatus = new OperationsStatus();
            //Product p = _productBusiness.GetProduct(1001, myStatus);
            //suv test
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

        //[HttpGet]
        //public string GetAllProductsByID(int id)
        //{
        //    try
        //    {
        //        OperationsStatusViewModel operationsStatusstatusObj=new OperationsStatusViewModel();
        //        List<ProductViewModel> productList = Mapper.Map<List<Product>,List<ProductViewModel>>(_productBusiness.GetProduct(id, Mapper.Map<OperationsStatusViewModel,OperationsStatus>(operationsStatusstatusObj)));

        //        return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
        //    }
        //    // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        //}

        [HttpPost]
        public string ProductInsert(ProductViewModel productObj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productObj.commonObj = new CommonViewModel();
                    //Getting UA
                    productObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName; 
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