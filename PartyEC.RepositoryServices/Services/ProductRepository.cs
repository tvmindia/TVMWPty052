using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PartyEC.RepositoryServices.Services
{
    public class ProductRepository:IProductRepository
    {
      private IDatabaseFactory _databaseFactory;
      private SqlConnection _con;
      private SqlCommand _cmd;

        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public ProductRepository(IDatabaseFactory databaseFactory)
      {
          _databaseFactory = databaseFactory;
      }

        /// <summary>
        /// Only Header detail list is required here
        /// </summary>
        /// <param name="productObj"></param>
        /// <returns></returns>
      public List<Product> GetAllProducts(Product productObj)
      {
          List<Product> productList = null;
          try
          {
              using (SqlConnection con = _databaseFactory.GetDBConnection())
              {
                  using (SqlCommand cmd = new SqlCommand())
                  {
                      if (con.State == ConnectionState.Closed)
                      {
                          con.Open();
                      }
                      cmd.Connection = con;
                      cmd.CommandText = "[GetAllProducts]";
                      cmd.CommandType = CommandType.StoredProcedure;
                      using (SqlDataReader sdr = cmd.ExecuteReader())
                      {
                          if ((sdr != null) && (sdr.HasRows))
                          {
                              productList = new List<Product>();
                              while (sdr.Read())
                              {
                                  Product _productObj = new Product();
                                  {
                                      _productObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : productObj.ID);
                                      _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : productObj.Name);
                                      _productObj.ShortDescription = sdr["ShortDescription"].ToString();
                                      _productObj.ProductType = Char.Parse(sdr["ProductType"].ToString());
                                    
                                  }
                                  productList.Add(_productObj);
                              }
                          }//if
                      }
                  }
              }
          }

          catch (Exception ex)
          {
              throw ex;
          }

          return productList;


      }
      public OperationsStatus InsertProduct(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
          try
            {
               SqlParameter outparameter = null;
               using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertProduct]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = productObj.Name;
                        cmd.Parameters.Add("@ShortDescription", SqlDbType.NVarChar,250).Value = productObj.ShortDescription;
                        cmd.Parameters.Add("@ProductType", SqlDbType.NVarChar, 1).Value = productObj.ProductType;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // not Successfull
                                
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
          return operationsStatusObj;
        }

        /// <summary>
        /// Header and detail will be selected
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public Product GetProduct(int ProductID, OperationsStatus Status)
        {
            Product myProduct = null;
            try
            {
                using (_con = _databaseFactory.GetDBConnection()) { 
                    if (_con.State == ConnectionState.Closed)
                    {
                        _con.Open();
                    }

                    myProduct = GetProductHeader(ProductID);
                    myProduct.ProductDetail = GetProductDetail(ProductID);
                }

            }
            catch (Exception e)
            {
                Status.StatusCode = -1;
                Status.StatusMessage = e.Message;
                Status.Exception = e;

            }
            

            return myProduct;
        }


        private Product GetProductHeader(int ProductID) {
            Product myProduct = null;
            try
            {
                _cmd = new SqlCommand();
                _cmd.Connection = _con;
                _cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                _cmd.CommandText = "[GetProductHeader]";
                _cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sdr = _cmd.ExecuteReader())
                {
                    if ((sdr != null) && (sdr.HasRows))
                    {
                        if (sdr.Read())
                        {
                            myProduct = new Product();
                            myProduct.ID = sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myProduct.ID;
                            myProduct.Name = sdr["Name"].ToString();
                            myProduct.SKU = sdr["SKU"].ToString();
                            myProduct.Enabled = (bool)(sdr["EnableYN"].ToString()!=""? sdr["EnableYN"]:false);
                            myProduct.Unit = sdr["Unit"].ToString();
                            myProduct.TaxClass = sdr["TaxClass"].ToString();
                            myProduct.URL = sdr["URL"].ToString();
                            myProduct.ShowPrice = (bool)(sdr["ShowPriceYN"].ToString() != "" ? sdr["ShowPriceYN"] : false);
                            myProduct.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()): myProduct.ActionType);

                            myProduct.SupplierID = sdr["SupplierID"].ToString() != "" ? Int16.Parse(sdr["SupplierID"].ToString()) : myProduct.SupplierID;
                            myProduct.SupplierID = sdr["ManufacturerID"].ToString() != "" ? Int16.Parse(sdr["ManufacturerID"].ToString()) : myProduct.ManufacturerID;
                            myProduct.SupplierName = sdr["SupplierName"].ToString();
                            myProduct.ManufacturerName = sdr["ManufacturerName"].ToString();
                            myProduct.BaseSellingPrice = sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : myProduct.BaseSellingPrice;
                            myProduct.CostPrice = sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : myProduct.CostPrice;
                            myProduct.ShortDescription = sdr["ShortDescription"].ToString();
                            myProduct.LongDescription = sdr["LongDescription"].ToString();
                            myProduct.ProductType =   (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : myProduct.ProductType); 
                            myProduct.StockAvailable = (bool)(sdr["StockAvailableYN"].ToString() != "" ? sdr["StockAvailableYN"] : false);  
                            myProduct.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? Int16.Parse(sdr["AttributeSetID"].ToString()) : myProduct.AttributeSetID;
                            myProduct.FreeDelivery = (bool)(sdr["FreeDeliveryYN"].ToString() != "" ? sdr["FreeDeliveryYN"] : false);  
                            //HeaderTag myProduct.ManufacturerName = sdr["ManufacturerName"].ToString();
                            myProduct.HeaderTags = sdr["HeaderTag"].ToString();
                            myProduct.StickerURL = sdr["StickerURL"].ToString();

                            myProduct.ProductHeaderImages = GetProductImages(myProduct.ID);

                            myProduct.LogDetails.CreatedBy = sdr["CreatedBy"].ToString();
                            myProduct.LogDetails.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["CreatedDate"].ToString())) : myProduct.LogDetails.CreatedDate);
                            myProduct.LogDetails.UpdatedBy = sdr["UpdatedBy"].ToString();
                            myProduct.LogDetails.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["UpdatedDate"].ToString())) : myProduct.LogDetails.UpdatedDate);

                        }

                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return myProduct;
        }

        private List<ProductDetail> GetProductDetail(int ProductID)
        {
            List<ProductDetail> myProductDetails = null;
            AttributesRepository myAttributesRepository = new AttributesRepository(_databaseFactory);
            List<AttributeValues> myAttributeStructure = null;
            try
            {
                _cmd = new SqlCommand();
                _cmd.Connection = _con;
                _cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                _cmd.CommandText = "[GetProductDetail]";
                _cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader sdr = _cmd.ExecuteReader())
                {
                    if ((sdr != null) && (sdr.HasRows))
                    {
                        myProductDetails = new List<ProductDetail>();
                        while (sdr.Read())
                        {
                            ProductDetail myProductDetail = new ProductDetail();
                            myProductDetail.ProductID = ProductID;
                            myProductDetail.ID = sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myProductDetail.ID;
                            myProductDetail.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? Int16.Parse(sdr["AttributeSetID"].ToString()) : myProductDetail.AttributeSetID;
                            myProductDetail.Qty= sdr["Qty"].ToString() != "" ? Int16.Parse(sdr["Qty"].ToString()) : myProductDetail.Qty; //sdr["Qty"].ToString();
                            myProductDetail.OutOfStockAlertQty = sdr["OutOfStockAlertQty"].ToString() != "" ? Int16.Parse(sdr["OutOfStockAlertQty"].ToString()) : myProductDetail.OutOfStockAlertQty;
                            myProductDetail.PriceDifference = sdr["PriceDiffAmt"].ToString() != "" ? decimal.Parse(sdr["PriceDiffAmt"].ToString()) : myProductDetail.PriceDifference;
                            myProductDetail.Enabled= (bool)(sdr["EnableYN"].ToString() != "" ? sdr["EnableYN"] : false);
                            myProductDetail.DetailTags = sdr["DetailTag"].ToString();
                            myProductDetail.StockAvailable = (bool)(sdr["StockAvailableYN"].ToString() != "" ? sdr["StockAvailableYN"] : false);
                            myProductDetail.DiscountAmount= sdr["DiscountAmout"].ToString() != "" ? decimal.Parse(sdr["DiscountAmout"].ToString()) : myProductDetail.DiscountAmount;
                            myProductDetail.DiscountStartDate = (sdr["DiscountStDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountStDate"].ToString())) : myProductDetail.DiscountStartDate);
                            myProductDetail.DiscountEndDate = (sdr["DiscountEnDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountEnDate"].ToString())) : myProductDetail.DiscountEndDate);

                            myProductDetail.LogDetails.CreatedBy = sdr["CreatedBy"].ToString();
                            myProductDetail.LogDetails.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["CreatedDate"].ToString())) : myProductDetail.LogDetails.CreatedDate);
                            myProductDetail.LogDetails.UpdatedBy = sdr["UpdatedBy"].ToString();
                            myProductDetail.LogDetails.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["UpdatedDate"].ToString())) : myProductDetail.LogDetails.UpdatedDate);

                            if (myAttributeStructure == null){
                                myAttributeStructure =myAttributesRepository.GetAttributeContainer(myProductDetail.AttributeSetID, "Product");
                            }
                                                  

                           foreach (AttributeValues att in myAttributeStructure)
                            {
                                AttributeValues myAttribute = new AttributeValues(att);//copy the values
                                try
                                {
                                    if(sdr.GetOrdinal(att.Name)>0)
                                    myAttribute.Value = sdr[att.Name].ToString();
                                }
                                catch (Exception)
                                {}
                                myProductDetail.ProductAttributes.Add(myAttribute);
                            }
                            myProductDetails.Add(myProductDetail);
                            myProductDetail.ProductDetailImages = GetProductImages(myProductDetail.ProductID,myProductDetail.ID);
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;

            }

            return myProductDetails;
        }

        private List<ProductImages> GetProductImages(int ProductID) {
            List<ProductImages> myImages = null;
            try
            {
                myImages= GetPrdImg(ProductID, -1);
            }
            catch (Exception e)
            {
                throw e;
            }
            return myImages;
        }
        private List<ProductImages> GetProductImages(int ProductID, int ProductDetailID)
        {
            List<ProductImages> myImages = null;
            try
            {
                myImages= GetPrdImg(ProductID, ProductDetailID);
            }
            catch (Exception e)
            {

                throw e;
            }
            return myImages;

        }      

        private List<ProductImages> GetPrdImg(int ProductID, int ProductDetailID = -1) {
            List<ProductImages> myImagesList = null;
            SqlConnection myCon;
            try
            {
               
                using(myCon = _databaseFactory.GetDBConnection()){
                    if (myCon.State == ConnectionState.Closed)
                    {
                        myCon.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = myCon;
                    cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                    cmd.Parameters.Add("@ProductDetailID", SqlDbType.Int).Value = ProductDetailID;
                    cmd.CommandText = "[GetProductImages]";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if ((sdr != null) && (sdr.HasRows))
                        {
                            myImagesList = new List<ProductImages>();
                            while (sdr.Read())
                            {
                                ProductImages myImage = new ProductImages();
                                myImage.URL = sdr["ImageURL"].ToString();
                                myImage.isMain = (bool)(sdr["MainImageYN"].ToString() != "" ? sdr["MainImageYN"] : false);
                                myImagesList.Add(myImage);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;  
            }
             
            return myImagesList;
        }
       

    }
}