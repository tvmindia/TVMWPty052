﻿using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
   public interface IProductRepository
   {
       List<Product> GetAllProducts(Product productObj);
        List<Product> GetAllProductswithCategory(string CategoryID);
        List<Product> GetAssignedPro(string CategoryID);
        List<Product> GetUnAssignedPro(string CategoryID);
        List<ProductDetail> GetAllReorderItems();
        OperationsStatus InsertProduct(Product productObj);
        OperationsStatus AddOrRemoveProductCategoryLink(List<ProductCategoryLink> AddList, List<ProductCategoryLink> DeleteList);
        Product GetProduct(int ProductID, OperationsStatus Status);
        Product GetProductHeader(int ProductID);
        List<Product> GetRelatedImages(int ProductID, OperationsStatus Status);
        OperationsStatus UpdateProduct(Product productObj);
        OperationsStatus UpdateProductSticker(Product productObj);
        List<Product> GetRelatedProducts(int productID);
        List<Product> GetUNRelatedProducts(int productID);
        OperationsStatus InsertRelatedProducts(Product productObj,string IDList);
        OperationsStatus DeleteRelatedProducts(Product productObj, string IDList);

        OperationsStatus InsertUpdateProductDetails(Product productObj);
        OperationsStatus UpdateProductHeaderOtherAttributes(Product productObj);
        List<ProductDetail> GetProductDetail(int ProductID,DateTime CurrentDate);
        List<ProductDetail> GetAllProductDetail(int LocationID,DateTime Datenow);
        ProductDetail GetProductDetailsByProduct(int ProductID,int DetailID);
        OperationsStatus DeleteProductsDetails(int ProductDetailsID,int ProductID);
        OperationsStatus DeleteProductImage(int ID);
        OperationsStatus InsertImageProduct(Product productObj);

        List<ProductReview> GetProductReviews(int ProductID);
        List<ProductReview> GetRatingSummary(int ProductID, int AttributesetId);

        List<Product> GetTopProductsOfCategory(Categories categoryObj);
        List<Product> GetProductsOfCategory(Categories categoryObj, DateTime currentDateTime);
        List<Product> GetProductsByFiltering(FilterCriteria filterCritiria, DateTime currentDateTime);
        List<Product> ProductsGlobalSearching(FilterCriteria filterCritiria, DateTime currentDateTime);
        Product GetProductDetailsForApp(int productID, DateTime currentDateTime, int customerID);
        Product GetProductSticker(int productID);
        List<ProductImages> GetProductImagesforApp(int ProductID);
        OperationsStatus UpdateWishlist(Wishlist CartWishObj);
        OperationsStatus UpdateRating(ProductReview ReviewObj);
        OperationsStatus InsertRating(ProductReview ReviewObj);
        OperationsStatus InsertReview(ProductReview ReviewObj);
        List<ProductReview> GetCustomerProductRating(int ProductID, int CustomerID, int AttributesetId);
        List<ProductReview> GetCustomerProductReview(int ProductID, int CustomerID);
        
    }
}
