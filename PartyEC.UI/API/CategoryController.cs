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
                List<CategoriesAppViewModel> CategoryList = Mapper.Map<List<Categories>, List< CategoriesAppViewModel >> (_categoryBusiness.GetAllMainCategories());
                if (CategoryList.Count==0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CategoryList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }



    }
}
