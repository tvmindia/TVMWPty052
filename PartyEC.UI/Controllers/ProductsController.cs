﻿using AutoMapper;
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
        IMasterBusiness _masterBusiness;
        IAttributeSetBusiness _attributeSetBusiness;
        public ProductsController(IProductBusiness productBusiness,IMasterBusiness masterBusiness,ICommonBusiness commonBusiness, IAttributeSetBusiness attributeSetBusiness)
        {
            _productBusiness = productBusiness;
            _masterBusiness = masterBusiness;
            _commonBusiness = commonBusiness;
            _attributeSetBusiness = attributeSetBusiness;
        }

        // GET: Products
        public ActionResult Index()
        {
            //suv test
            //OperationsStatus myStatus = new OperationsStatus();
            //Product p = _productBusiness.GetProduct(1001, myStatus);
            //suv test

            //Drop BInd
            ProductViewModel product = null;
            try
            {
                product = new ProductViewModel();
                List<SelectListItem> selectListItem = new List<SelectListItem>();
                //Supplier Drop down bind
                List<SupplierViewModel> supplierListVM=Mapper.Map<List<Supplier>, List<SupplierViewModel>>(_masterBusiness.GetAllSuppliers());
                foreach(SupplierViewModel svm in supplierListVM)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = svm.Name,
                        Value = svm.ID.ToString(),
                        Selected = false
                    });
                }
                product.suppliers = selectListItem;
                //Manufacturer Drop down bind
                List<ManufacturerViewModel> manfactureListVM=Mapper.Map<List<Manufacturer>,List<ManufacturerViewModel>>(_masterBusiness.GetAllManufacturers());
                selectListItem = null;
                selectListItem = new List<SelectListItem>();
                foreach (ManufacturerViewModel mvm in manfactureListVM)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = mvm.Name,
                        Value = mvm.ID.ToString(),
                        Selected = false
                    });
                }
                product.manufacturers = selectListItem;

                //AttributeSet Drop down bind
                List<AttributeSetViewModel> AttributeSetListVM = Mapper.Map<List<AttributeSet>, List<AttributeSetViewModel>>(_attributeSetBusiness.GetAllAttributeSet());
                selectListItem = null;
                selectListItem = new List<SelectListItem>();
                foreach (AttributeSetViewModel avm in AttributeSetListVM)
                {
                    selectListItem.Add(new SelectListItem
                    {
                        Text = avm.Name,
                        Value = avm.ID.ToString(),
                        Selected = false
                    });
                }
                product.AttributeSets = selectListItem;
            }
            catch(Exception ex)
            {

            }
            

            return View(product);
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
         
        }

        [HttpGet]
        public string GetRelatedProducts(string id)
        {
            try
            {
                if(!string.IsNullOrEmpty(id))
                {
                    List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetRelatedProducts(int.Parse(id)));

                    return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
                }
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = "id is empty" });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
           
        }
        [HttpGet]
        public string GetAllProductswithCategory(ProductViewModel productObj)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAllProductswithCategory(Mapper.Map<ProductViewModel, Product>(productObj)));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpGet]
        public string GetAssignedPro(string CategoryID)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetAssignedPro(CategoryID));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }
        [HttpGet]
        public string GetUnAssignedPro(string CategoryID)
        {
            try
            {
                List<ProductViewModel> productList = Mapper.Map<List<Product>, List<ProductViewModel>>(_productBusiness.GetUnAssignedPro(CategoryID));

                return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
            // return JsonConvert.SerializeObject(new { Result = "OK", Records = productList });
        }

        [HttpGet]
        public string GetProduct(string id)
        {
            try
            {
                OperationsStatusViewModel operationsStatus = new OperationsStatusViewModel();
                ProductViewModel product = Mapper.Map<Product,ProductViewModel>(_productBusiness.GetProduct(Int32.Parse(id), Mapper.Map<OperationsStatusViewModel, OperationsStatus>(operationsStatus)));
                return JsonConvert.SerializeObject(new { Result = "OK", Record = product });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public string ProductInsertUpdate(ProductViewModel productObj)
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