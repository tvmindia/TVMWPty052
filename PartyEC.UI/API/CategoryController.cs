using System;
using System.Collections.Generic;
using System.Web.Http;
using PartyEC.UI.Models;
using PartyEC.BusinessServices.Contracts;
using Newtonsoft.Json;
using AutoMapper;
using PartyEC.DataAccessObject.DTO;

namespace PartyEC.UI.API
{
    public class CategoryController : ApiController
    {

        #region Constructor_Injection

        ICategoriesBusiness _categoryBusiness;
        ICommonBusiness _commonBusiness;
        IProductBusiness _productBusiness;

        public CategoryController(ICategoriesBusiness categoryBusiness, ICommonBusiness commonBusiness, IProductBusiness productBusiness)
        {
            _categoryBusiness = categoryBusiness;
            _commonBusiness = commonBusiness;
            _productBusiness = productBusiness;
        }
        #endregion Constructor_Injection
        Const messages= new Const();

        [HttpPost]
        public object GetMainCategories()
        {
            try
            {                
                List<CategoriesListAppViewModel> CategoryList = Mapper.Map<List<Categories>, List< CategoriesListAppViewModel >> (_categoryBusiness.GetAllMainCategories());
                if (CategoryList.Count==0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CategoryList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCategoryMainPageItems(Categories categoryObj)
        {
            try
            {
                List<TopProductsOfCategoryAppViewModel> ProductsList = Mapper.Map<List<Product>, List<TopProductsOfCategoryAppViewModel>>(_productBusiness.GetTopProductsOfCategory(categoryObj));
                List<NavigationalCatsOfCategoryAppViewModel> CategoryList = Mapper.Map<List<Categories>, List<NavigationalCatsOfCategoryAppViewModel>>(_categoryBusiness.GetNavigationalCategoriesForApp(categoryObj));
                if (ProductsList.Count == 0 && CategoryList.Count ==0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = new { Products = ProductsList, SubCategories =  CategoryList} });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetProductsOfCategory(Categories categoryObj)
        {
            try
            {
                List<ProductsOfCategoryAppViewModel> ProductsList = Mapper.Map<List<Product>, List<ProductsOfCategoryAppViewModel>>(_productBusiness.GetProductsOfCategory(categoryObj));
                List<FilterCatsOfCategoryAppViewModel> CategoryList = Mapper.Map<List<Categories>, List<FilterCatsOfCategoryAppViewModel>>(_categoryBusiness.GetFilterCategoriesForApp(categoryObj));
                if (ProductsList.Count == 0 && CategoryList.Count == 0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = new { Products = ProductsList, SubCategories = CategoryList } });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }



    }
}
