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
                myProduct = GetProductHeader(ProductID);
                myProduct.ProductDetail = GetProductDetail(ProductID);

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
            try
            {

            }
            catch (Exception e)
            {
                throw e;

            }

            return myProductDetails;
        }

    }
}