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
                                      //_productObj.ProductType = Char.Parse(sdr["ProductType"].ToString();
                                    
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
    }
}