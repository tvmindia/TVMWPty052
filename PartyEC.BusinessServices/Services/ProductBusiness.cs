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
        private IMasterRepository _masterRepository;
        private ICommonBusiness _commonBusiness;
        public ProductBusiness(IProductRepository productRepository,IMasterRepository masterRepository,ICommonBusiness commonBusiness)
        {
            _productRepository = productRepository;
            _masterRepository = masterRepository;
            _commonBusiness = commonBusiness;
        }

        public List<Product> GetAllProducts(Product productObj)
        {
            List<Product> productlist = null;
            try
            {
              productlist = _productRepository.GetAllProducts(productObj).OrderByDescending(prod => prod.logDetails.CreatedDate).ToList();
            }
            catch(Exception)
            {

            }
            return productlist;
        }

        public List<Product> GetTop10Products()
        {
            Product productObj = new Product();
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetAllProducts(productObj).OrderByDescending(prod => prod.logDetails.CreatedDate).Take(10).ToList();
            }
            catch (Exception)
            {

            }
            return productlist;
        }

        public List<ProductDetail> GetAllReorderItems()
        {
            return _productRepository.GetAllReorderItems();
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
        /// <summary>
        /// new product insertion
        /// for product type simple there will be product details
        /// for product type configurable ther will be no product details
        /// </summary>
        /// <param name="productObj"></param>
        /// <returns>OperationsStatus</returns>
        public OperationsStatus InsertProduct(Product productObj)
        {
            OperationsStatus operationsStatus = null;
            try
            {
                switch(productObj.ProductType)
                {
                    case 'S':
                        productObj.ProductDetails = productObj.ProductDetails == null ? null : productObj.ProductDetails.
                        Select(prodDet => { prodDet.DefaultOption = true; return prodDet; }).ToList();
                        operationsStatus = _productRepository.InsertProduct(productObj);
                        break;
                    case 'C':
                        //product detail not need to insert for configurable product
                        productObj.ProductDetails.Clear();
                        operationsStatus = _productRepository.InsertProduct(productObj);
                        break;
                }
              
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return operationsStatus;
        }

        public OperationsStatus UpdateProduct(Product productObj)
        {
            OperationsStatus operationsStatus = null;
            try
            {
                switch(productObj.ProductType)
                {
                    case 'S':
                        productObj.ProductDetails = productObj.ProductDetails == null ? null : productObj.ProductDetails.
                        Select(prodDet => { prodDet.DefaultOption = true; return prodDet; }).ToList();
                        operationsStatus = _productRepository.UpdateProduct(productObj);
                        break;
                    case 'C':
                        productObj.ProductDetails.Clear();
                        operationsStatus = _productRepository.UpdateProduct(productObj);
                        break;
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return operationsStatus;
        }

        public OperationsStatus UpdateProductSticker(Product productObj)
        {
            return _productRepository.UpdateProductSticker(productObj);
        }

        public Product GetProduct(int ProductID, OperationsStatus Status)
        {

            return _productRepository.GetProduct(ProductID, Status);
        }

        public List<AttributeValues> GetAttributeValuesByProduct(int ProductID)
        {
            List<AttributeValues> attrvalues = null;
            try
            {
                attrvalues= _productRepository.GetProductHeader(ProductID).ProductOtherAttributes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return attrvalues;
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
                productDetailslist = productDetailslist==null?null: productDetailslist.OrderByDescending(prod => prod.logDetails.CreatedDate).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productDetailslist;
        }
        public List<ProductDetail> GetAllProductDetail(int LocationID)
        {
            List<ProductDetail> productDetailslist = null;
            try
            {

                productDetailslist = _productRepository.GetAllProductDetail(LocationID, _commonBusiness.GetCurrentDateTime());

            }
            catch (Exception ex)
            {
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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

        public List<ProductReview> GetRatingSummary(int ProductID, int AttributesetId)
        {
            List<ProductReview> RatingSummary = null;
            try
            {
                RatingSummary = _productRepository.GetRatingSummary(ProductID, AttributesetId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RatingSummary;
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

        public OperationsStatus InsertStickers(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                //if (productObj.StickerURL != "" && productObj.StickerURL != null)
                //{
                    OtherImages otherImgObj = new OtherImages();
                    otherImgObj.URL = productObj.StickerURL;
                    otherImgObj.ImageType = ImageTypesPreffered.Sticker;
                    otherImgObj.LogDetails = productObj.logDetails;
                    operationsStatusObj = _masterRepository.InsertImage(otherImgObj);
                    //productObj.StickerID = operationsStatusObj.ReturnValues.ToString();
                //}
                //operationsStatusObj = _categoryRepository.UpdateCategory(CategoryObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }


        #region For App

        public List<Product> GetTopProductsOfCategory(Categories categoryObj)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetTopProductsOfCategory(categoryObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productlist;
        }

        public List<Product> GetProductsOfCategory(Categories categoryObj)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetProductsOfCategory(categoryObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productlist;
        }

        public List<Product> GetProductsByFiltering(FilterCriteria filterCriteria)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetProductsByFiltering(filterCriteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productlist;
        }

        public Product GetProductDetailsForApp(int productID, DateTime currentDateTime, int customerID)
        {
            Product product = null;
            try
            {
                product = _productRepository.GetProductDetailsForApp(productID, currentDateTime, customerID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return product;
        }

        public List<ProductReview> GetProductReviewsForApp(int ProductID,int count)
        {
            List<ProductReview> productReview = null;
            try
            {
                productReview = _productRepository.GetProductReviews(ProductID).OrderByDescending(prodR => prodR.ReviewCreatedDate).ToList();
                if (count != -1 && count<=productReview.Count)  //taking only top reviews sorted by date
                {
                    productReview = productReview.GetRange(0,count);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productReview;
        }

        public List<Product> GetRelatedProductsForApp(int productID,int count)
        {
            List<Product> productlist = null;
            try
            {
                productlist = _productRepository.GetRelatedProducts(productID);
                if (count != -1 && count <= productlist.Count)  //taking only top items
                {
                    productlist = productlist.GetRange(0, count);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return productlist;
        }

        public Product GetProductForApp(int ProductID, OperationsStatus Status)
        {
             Product  product = null;
            try
            {
                product = _productRepository.GetProduct(ProductID, Status);
                int count = product.ProductDetails.Count;
                for (int i= 0;i< count;i++)
                {
                    decimal? discount = product.ProductDetails[i].DiscountAmount;
                    if (discount!=null && discount != 0)
                    {
                        DateTime? startdate=null, enddate=null,today;
                        if(product.ProductDetails[i].DiscountStartDate!=null)
                            startdate = DateTime.Parse(product.ProductDetails[i].DiscountStartDate);// Convert.ToDateTime(product.ProductDetails[i].DiscountStartDate);
                        if(product.ProductDetails[i].DiscountEndDate!=null)
                            enddate = DateTime.Parse(product.ProductDetails[i].DiscountEndDate);
                        today = DateTime.Now ;
                        if(startdate!=null && enddate != null)
                        {
                            if (startdate<=today && enddate >= today)
                            {
                                product.ProductDetails[i].DiscountAmount = discount;
                            }
                            else
                            {
                                product.ProductDetails[i].DiscountAmount = 0;
                            }
                        }
                        else if (startdate != null )
                        {
                            if (startdate <= today)
                            {
                                product.ProductDetails[i].DiscountAmount = discount;
                            }
                            else
                            {
                                product.ProductDetails[i].DiscountAmount = 0;
                            }
                        }
                        else if (enddate != null)
                        {
                            if ( enddate >= today)
                            {
                                product.ProductDetails[i].DiscountAmount = discount;
                            }
                            else
                            {
                                product.ProductDetails[i].DiscountAmount = 0;
                            }
                        }
                        else
                        {
                            product.ProductDetails[i].DiscountAmount = discount;
                        }
                    }
                    else
                    {
                        product.ProductDetails[i].DiscountAmount = 0;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return product; 
        }
        
        public Product GetProductSticker(int productID)
        {
            Product product = null;
            try
            {
                product = _productRepository.GetProductSticker(productID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return product;
        }
       
        public List<ProductImages> GetProductImagesforApp(int ProductID)
        {

            List<ProductImages> productImages = null;
            try
            {
                productImages = _productRepository.GetProductImagesforApp(ProductID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productImages;
        }

        public OperationsStatus UpdateWishlist(Wishlist CartWishObj)
        {
            OperationsStatus OSatObj = null;
            try
            {
                OSatObj = _productRepository.UpdateWishlist(CartWishObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OSatObj;
        }

        
        #endregion
    }
}