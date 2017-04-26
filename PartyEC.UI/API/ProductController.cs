using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
                productObj.AttributeSetID = product.AttributeSetID;
                return JsonConvert.SerializeObject(new { Result = true, Records = productApp });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public object GetProductRatings(Product productObj)
        {
            try
            {
                List<ProductReviewViewModel> productRating = Mapper.Map<List<ProductReview>, List<ProductReviewViewModel>>(_productBusiness.GetRatingSummary(productObj.ID,productObj.AttributeSetID));
                if (productRating.Count == 0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = productRating });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public object GetProductReviews(ProductReviewAppViewModel productObj)
        {
            try
            {
                List<ProductReviewAppViewModel> productReviews = Mapper.Map<List<ProductReview>, List<ProductReviewAppViewModel>>(_productBusiness.GetProductReviewsForApp(productObj.ProductID,productObj.count));
                if (productReviews.Count == 0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = productReviews });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public object GetRelatedProducts(RelatedProductsAppViewModel productObj)
        {
            try
            {
                List<RelatedProductsAppViewModel> relatedProducts = Mapper.Map<List<Product>, List<RelatedProductsAppViewModel>>(_productBusiness.GetRelatedProductsForApp(productObj.ID, productObj.count));
                if (relatedProducts.Count == 0) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = relatedProducts });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        //[HttpPost]
        //public object GetProductDetailsForOrder(Product productObj)
        //{
        //    try
        //    {
        //        ProductViewModel productDetailsForOrder = Mapper.Map<Product, ProductViewModel>(_productBusiness.GetProduct(productObj.ID,new OperationsStatus()));
        //        return JsonConvert.SerializeObject(new { Result = true, Records = productDetailsForOrder });
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
        //    }
        //}
        [HttpPost]
        public object GetProductDetailsForOrder(Product productObj)
        {
            try
            {
                ProductAppViewModel GetProductForApp = Mapper.Map<Product, ProductAppViewModel>(_productBusiness.GetProductForApp(productObj.ID, new OperationsStatus()));
                return JsonConvert.SerializeObject(new { Result = true, Records = GetProductForApp });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public object GetProductImages(Product productObj)
        {
            OperationsStatus Status = new OperationsStatus();
            try
            {
                //[GetProductImages]
                List<ProductImagesViewModel> ProductImages = Mapper.Map<List<ProductImages>, List<ProductImagesViewModel>>(_productBusiness.GetProductImagesforApp(productObj.ID));
                ProductAppViewModel ProductImageStickers = Mapper.Map<Product,ProductAppViewModel>(_productBusiness.GetProductSticker(productObj.ID));
                if (ProductImages.Count == 0 && ProductImageStickers!= null) throw new Exception(messages.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = new { Images= ProductImages, Stickers = ProductImageStickers } });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object UpdateWishlist(Cart_Wishlist wishlistObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                wishlistObj.logDetails = new LogDetails();
                wishlistObj.logDetails.CreatedBy = _commonBusiness.GetUA().UserName;
                wishlistObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus,OperationsStatusViewModel>(_productBusiness.UpdateWishlist(wishlistObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

    }
}
