using PartyEC.DataAccessObject.DTO;
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
        List<ProductDetail> GetProductDetail(int ProductID);
        List<ProductDetail> GetAllProductDetail();
        ProductDetail GetProductDetailsByProduct(int ProductID,int DetailID);
        OperationsStatus DeleteProductsDetails(int ProductDetailsID,int ProductID);
        OperationsStatus DeleteProductImage(int ID);
        OperationsStatus InsertImageProduct(Product productObj);

        List<ProductReview> GetProductReviews(int ProductID);
        List<ProductReview> GetRatingSummary(int ProductID, int AttributesetId);

        List<Product> GetTopProductsOfCategory(Categories categoryObj);
        List<Product> GetProductsOfCategory(Categories categoryObj);
        List<Product> GetProductsByFiltering(FilterCriteria filterCritiria);
        Product GetProductDetailsForApp(Product productObj, DateTime currentDateTime);
        Product GetProductSticker(int productID);
        List<ProductImages> GetProductImagesforApp(int ProductID);
        OperationsStatus UpdateWishlist(Wishlist CartWishObj);
    }
}
