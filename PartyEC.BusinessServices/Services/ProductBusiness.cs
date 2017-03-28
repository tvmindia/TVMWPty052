using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class ProductBusiness:IProductBusiness
    {
        private IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts(Product productObj)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetAllProducts(productObj);

            }
            catch(Exception)
            {

            }
            return productlist;
        }
        public List<Product> GetAllProductswithCategory(string CategoryID)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetAllProductswithCategory(CategoryID);

            }
            catch (Exception)
            {

            }
            return productlist;
        }
        public List<Product> GetAssignedPro(string CategoryID)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetAssignedPro(CategoryID);

            }
            catch (Exception)
            {

            }
            return productlist;
        }
        public List<Product> GetUnAssignedPro(string CategoryID)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetUnAssignedPro(CategoryID);

            }
            catch (Exception)
            {

            }
            return productlist;
        }

         public List<Product> GetRelatedProducts(int productID)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetRelatedProducts(productID);

            }
            catch (Exception)
            {

            }
            return productlist;
        }

        public List<Product> GetUNRelatedProducts(int productID)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetUNRelatedProducts(productID);

            }
            catch (Exception)
            {

            }
            return productlist;
        }


        public OperationsStatus InsertProduct(Product productObj)
        {
            
              return _productRepository.InsertProduct(productObj);
            
        }
        public OperationsStatus UpdateProduct(Product productObj)
        {
            return _productRepository.UpdateProduct(productObj);
        }

        public Product GetProduct(int ProductID, OperationsStatus Status)
        {

            return _productRepository.GetProduct(ProductID, Status);
        }
        public List<Product> GetRelatedImages(int ProductID, OperationsStatus Status)
        {

            return _productRepository.GetRelatedImages(ProductID, Status);
        }

        public OperationsStatus AddOrRemoveProductCategoryLink(List<ProductCategoryLink> AddList, List<ProductCategoryLink> DeleteList)
        {

            return _productRepository.AddOrRemoveProductCategoryLink(AddList,DeleteList);

        }

        public OperationsStatus InsertRelatedProducts(Product productObj, string IDList)
        {
            try
            {
                return _productRepository.InsertRelatedProducts(productObj, IDList);
            }
            catch (Exception ex)
            {
                OperationsStatus OS = new OperationsStatus();
                OS.StatusMessage = ex.Message.ToString();
                return OS;
            }
        }

        public OperationsStatus DeleteRelatedProducts(Product productObj, string IDList)
        {
            try
            {
                return _productRepository.DeleteRelatedProducts(productObj, IDList);
            }
            catch (Exception ex)
            {
                OperationsStatus OS = new OperationsStatus();
                OS.StatusMessage = ex.Message.ToString();
                return OS;
            }
        }


        public OperationsStatus InsertUpdateProductDetails(Product productObj)
        {
            try
            {
                return _productRepository.InsertUpdateProductDetails(productObj);
            }
            catch (Exception ex)
            {
                OperationsStatus OS = new OperationsStatus();
                OS.StatusMessage = ex.Message.ToString();
                return OS;
            }
        }


        public OperationsStatus UpdateProductHeaderOtherAttributes(Product productObj)
        {
            try
            {
                return _productRepository.UpdateProductHeaderOtherAttributes(productObj);
            }
            catch (Exception ex)
            {
                OperationsStatus OS = new OperationsStatus();
                OS.StatusMessage = ex.Message.ToString();
                return OS;
            }
        }


        public List<ProductDetail> GetProductDetail(int ProductID)
        {
            List<ProductDetail> productDetailslist = null;
            try
            {
                productDetailslist = _productRepository.GetProductDetail(ProductID);

            }
            catch (Exception)
            {

            }
            return productDetailslist;
        }

           public ProductDetail GetProductDetailsByProduct(int ProductID, int DetailID)
          {
            ProductDetail productDetail = null;
            try
            {
                productDetail = _productRepository.GetProductDetailsByProduct(ProductID, DetailID);
            }
            catch(Exception ex)
            {

            }
            return productDetail;
         }
         public  OperationsStatus DeleteProductsDetails(int ProductDetailsID, int ProductID)
         {
            OperationsStatus OS = null;
            try
            {
                OS=_productRepository.DeleteProductsDetails(ProductDetailsID, ProductID);
            }
            catch (Exception ex)
            {

            }
            return OS;
        }
        public OperationsStatus DeleteProductsImage(string[] DeleteIDs)
        {
            OperationsStatus OS = null;
            try
            {
                foreach(var i in DeleteIDs)
                {
                    OS = _productRepository.DeleteProductImage(int.Parse(i));
                }
                
            }
            catch (Exception ex)
            {

            }
            return OS;
        }

        public List<ProductReview> GetProductReviews(int ProductID)
        {
            List<ProductReview> productReview = null;
            try
            {
                productReview = _productRepository.GetProductReviews(ProductID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productReview;
        }
        public OperationsStatus InsertImageProduct(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                    operationsStatusObj = _productRepository.InsertImageProduct(productObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }

    }
}