using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IProductBusiness
    {
        List<Product> GetAllProducts(Product productObj);
        List<Product> GetAllProductswithCategory(string CategoryID);
        List<Product> GetAssignedPro(string CategoryID);
        List<Product> GetUnAssignedPro(string CategoryID);
        OperationsStatus InsertProduct(Product productObj);
        OperationsStatus UpdateProduct(Product productObj);
        OperationsStatus UpdateProductSticker(Product productObj);
        OperationsStatus AddOrRemoveProductCategoryLink(List<ProductCategoryLink> AddList,List<ProductCategoryLink> DeleteList);
        Product GetProduct(int ProductID, OperationsStatus Status);
        List<Product> GetRelatedImages(int ProductID, OperationsStatus Status);
        List<Product> GetRelatedProducts(int productID);
        List<Product> GetUNRelatedProducts(int productID);
        OperationsStatus InsertRelatedProducts(Product productObj, string IDList);
        OperationsStatus DeleteRelatedProducts(Product productObj, string IDList);
        OperationsStatus InsertUpdateProductDetails(Product productObj);
        OperationsStatus UpdateProductHeaderOtherAttributes(Product productObj);
        List<ProductDetail> GetProductDetail(int ProductID);
        ProductDetail GetProductDetailsByProduct(int ProductID, int DetailID);
        OperationsStatus DeleteProductsDetails(int ProductDetailsID, int ProductID);
        OperationsStatus DeleteProductsImage(string[] DeleteIDs);
        OperationsStatus InsertImageProduct(Product productObj);
        OperationsStatus InsertStickers(Product productObj);
        List<ProductReview> GetProductReviews(int ProductID);
        List<ProductReview> GetRatingSummary(int ProductID, int AttributesetId);

        List<Product> GetTopProductsOfCategory(Categories categoryObj);
        List<Product> GetProductsOfCategory(Categories categoryObj);
        List<Product> GetProductsByFiltering(FilterCriteria filterCriteria);
        Product GetProductDetailsForApp(Product productObj, DateTime currentDateTime);
        List<ProductReview> GetProductReviewsForApp(int ProductID, int count);
    }
}
