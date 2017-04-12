using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using PartyEC.UI.Models;

namespace PartyEC.UI.API
{
    public class ProductController : ApiController
    {
        #region Constructor_Injection

        ICategoriesBusiness _categoryBusiness;
        ICommonBusiness _commonBusiness;
        IProductBusiness _productBusiness;

        public ProductController(ICategoriesBusiness categoryBusiness, ICommonBusiness commonBusiness, IProductBusiness productBusiness)
        {
            _categoryBusiness = categoryBusiness;
            _commonBusiness = commonBusiness;
            _productBusiness = productBusiness;
        }
        #endregion Constructor_Injection
        Const messages = new Const();

        [HttpPost]
        public object GetProductDetails(Product productObj)
        {
            try
            {
                Product product=_productBusiness.GetProductDetailsForApp(productObj, _commonBusiness.GetCurrentDateTime());
                ProductAppViewModel productApp = new ProductAppViewModel();
                productApp.Name = product.Name;
                productApp.SKU = product.SKU;
                productApp.ShowPrice = product.ShowPrice;
                productApp.ActionType = product.ActionType;
                productApp.SupplierID = product.SupplierID;
                productApp.SupplierName = product.SupplierName;
                productApp.BaseSellingPrice = product.BaseSellingPrice;
                productApp.ShortDescription = product.ShortDescription;
                productApp.LongDescription = product.LongDescription;
                productApp.ProductType = product.ProductType;
                productApp.FreeDelivery = product.FreeDelivery;
                productApp.StickerURL = product.StickerURL;
                productApp.PriceDifference = product.ProductDetailObj.PriceDifference;
                productApp.StockAvailable = product.StockAvailable;
                productApp.DiscountAmount = product.ProductDetailObj.DiscountAmount;
                return JsonConvert.SerializeObject(new { Result = true, Records = productApp });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
    }
}
