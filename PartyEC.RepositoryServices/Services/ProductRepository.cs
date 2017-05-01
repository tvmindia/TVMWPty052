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
    public class ProductRepository : IProductRepository
    {
        private IDatabaseFactory _databaseFactory;
        private IAttributesRepository _attributesRepository;
        private SqlConnection _con;
        private SqlCommand _cmd;
        Const constObj = new Const();

        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public ProductRepository(IDatabaseFactory databaseFactory, IAttributesRepository attributesRepository)
        {
            _databaseFactory = databaseFactory;
            _attributesRepository = attributesRepository;
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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.SKU = (sdr["SKU"].ToString() != "" ? sdr["SKU"].ToString() : _productObj.SKU);
                                        _productObj.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : _productObj.Enabled);
                                        _productObj.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : _productObj.Unit);
                                        _productObj.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr[""].ToString() : _productObj.TaxClass);
                                        _productObj.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : _productObj.URL);
                                        _productObj.ImageURL = (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _productObj.ImageURL);
                                        _productObj.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : _productObj.ShowPrice);
                                        _productObj.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : _productObj.ActionType);
                                        _productObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : _productObj.SupplierID);
                                        _productObj.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? int.Parse(sdr["ManufacturerID"].ToString()) : _productObj.ManufacturerID);
                                        _productObj.BaseSellingPrice = (sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : _productObj.BaseSellingPrice);
                                        _productObj.CostPrice = (sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : _productObj.CostPrice);
                                        _productObj.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : _productObj.ShortDescription);
                                        _productObj.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : _productObj.LongDescription);
                                        _productObj.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : _productObj.ProductType);
                                        _productObj.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : _productObj.StockAvailable);
                                        _productObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : _productObj.AttributeSetID);
                                        _productObj.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : _productObj.FreeDelivery);
                                        _productObj.HeaderTags = (sdr["HeaderTag"].ToString() != "" ? sdr["HeaderTag"].ToString() : _productObj.HeaderTags);
                                        _productObj.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : _productObj.SupplierName);
                                        _productObj.ManufacturerName = (sdr["ManufacturerName"].ToString() != "" ? sdr["ManufacturerName"].ToString() : _productObj.ManufacturerName);
                                        _productObj.StickerURL = (sdr["StickerURL"].ToString() != "" ? sdr["StickerURL"].ToString() : _productObj.StickerURL);
                                        _productObj.TotalQty= (sdr["TotalQTY"].ToString() != "" ? int.Parse(sdr["TotalQTY"].ToString()) : _productObj.TotalQty);
                                        _productObj.logDetails = new LogDetails();

                                        _productObj.logDetails.CreatedDate = ((sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _productObj.logDetails.CreatedDate));
                                             _productObj.logDetails.CreatedBy = (sdr["CreatedBy"].ToString() != "" ? sdr["CreatedBy"].ToString() : _productObj.logDetails.CreatedBy);
                                        
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

        public List<Product> GetAllProductswithCategory(string CategoryID)
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
                        cmd.CommandText = "[GetAllProductswithCategory]";
                        if (CategoryID != "" && CategoryID != null)
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(CategoryID.ToString());
                        }
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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.SKU = (sdr["SKU"].ToString() != "" ? sdr["SKU"].ToString() : _productObj.SKU);
                                        _productObj.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : _productObj.Enabled);
                                        _productObj.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : _productObj.Unit);
                                        _productObj.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr[""].ToString() : _productObj.TaxClass);
                                        _productObj.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : _productObj.URL);
                                        _productObj.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : _productObj.ShowPrice);
                                        _productObj.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : _productObj.ActionType);
                                        _productObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : _productObj.SupplierID);
                                        _productObj.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? int.Parse(sdr["ManufacturerID"].ToString()) : _productObj.ManufacturerID);
                                        _productObj.BaseSellingPrice = (sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : _productObj.BaseSellingPrice);
                                        _productObj.CostPrice = (sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : _productObj.CostPrice);
                                        _productObj.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : _productObj.ShortDescription);
                                        _productObj.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : _productObj.LongDescription);
                                        _productObj.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : _productObj.ProductType);
                                        _productObj.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : _productObj.StockAvailable);
                                        _productObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : _productObj.AttributeSetID);
                                        _productObj.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : _productObj.FreeDelivery);
                                        _productObj.HeaderTags = (sdr["HeaderTag"].ToString() != "" ? sdr["HeaderTag"].ToString() : _productObj.HeaderTags);
                                        _productObj.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : _productObj.SupplierName);
                                        _productObj.CategoryID = (sdr["CategoryID"].ToString() != "" ? int.Parse(sdr["CategoryID"].ToString()) : _productObj.CategoryID);
                                        _productObj.PositionNo = (sdr["PositionNo"].ToString() != "" ? float.Parse(sdr["PositionNo"].ToString()) : _productObj.PositionNo);
                                        _productObj.LinkID = (sdr["LinkID"].ToString() != "" ? int.Parse(sdr["LinkID"].ToString()) : _productObj.LinkID);
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
        public List<Product> GetUnAssignedPro(string CategoryID)
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
                        cmd.CommandText = "[GetAllProductswithCategoryUnAssigned]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (CategoryID != "" && CategoryID != null)
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(CategoryID.ToString());
                        }
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                productList = new List<Product>();
                                while (sdr.Read())
                                {
                                    Product _productObj = new Product();
                                    {
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.SKU = (sdr["SKU"].ToString() != "" ? sdr["SKU"].ToString() : _productObj.SKU);
                                        _productObj.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : _productObj.Enabled);
                                        _productObj.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : _productObj.Unit);
                                        _productObj.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr[""].ToString() : _productObj.TaxClass);
                                        _productObj.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : _productObj.URL);
                                        _productObj.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : _productObj.ShowPrice);
                                        _productObj.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : _productObj.ActionType);
                                        _productObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : _productObj.SupplierID);
                                        _productObj.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? int.Parse(sdr["ManufacturerID"].ToString()) : _productObj.ManufacturerID);
                                        _productObj.BaseSellingPrice = (sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : _productObj.BaseSellingPrice);
                                        _productObj.CostPrice = (sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : _productObj.CostPrice);
                                        _productObj.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : _productObj.ShortDescription);
                                        _productObj.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : _productObj.LongDescription);
                                        _productObj.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : _productObj.ProductType);
                                        _productObj.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : _productObj.StockAvailable);
                                        _productObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : _productObj.AttributeSetID);
                                        _productObj.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : _productObj.FreeDelivery);
                                        _productObj.HeaderTags = (sdr["HeaderTag"].ToString() != "" ? sdr["HeaderTag"].ToString() : _productObj.HeaderTags);
                                        _productObj.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : _productObj.SupplierName);
                                        _productObj.CategoryID = (sdr["CategoryID"].ToString() != "" ? int.Parse(sdr["CategoryID"].ToString()) : _productObj.CategoryID);
                                        _productObj.PositionNo = (sdr["PositionNo"].ToString() != "" ? float.Parse(sdr["PositionNo"].ToString()) : _productObj.PositionNo);
                                        _productObj.LinkID = (sdr["LinkID"].ToString() != "" ? int.Parse(sdr["LinkID"].ToString()) : _productObj.LinkID);
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
        public List<Product> GetAssignedPro(string CategoryID)
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
                        cmd.CommandText = "[GetAllProductswithCategoryAssiged]";
                        if (CategoryID != "" && CategoryID != null)
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(CategoryID.ToString());
                        }
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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.SKU = (sdr["SKU"].ToString() != "" ? sdr["SKU"].ToString() : _productObj.SKU);
                                        _productObj.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : _productObj.Enabled);
                                        _productObj.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : _productObj.Unit);
                                        _productObj.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr[""].ToString() : _productObj.TaxClass);
                                        _productObj.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : _productObj.URL);
                                        _productObj.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : _productObj.ShowPrice);
                                        _productObj.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : _productObj.ActionType);
                                        _productObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : _productObj.SupplierID);
                                        _productObj.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? int.Parse(sdr["ManufacturerID"].ToString()) : _productObj.ManufacturerID);
                                        _productObj.BaseSellingPrice = (sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : _productObj.BaseSellingPrice);
                                        _productObj.CostPrice = (sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : _productObj.CostPrice);
                                        _productObj.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : _productObj.ShortDescription);
                                        _productObj.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : _productObj.LongDescription);
                                        _productObj.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : _productObj.ProductType);
                                        _productObj.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : _productObj.StockAvailable);
                                        _productObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : _productObj.AttributeSetID);
                                        _productObj.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : _productObj.FreeDelivery);
                                        _productObj.HeaderTags = (sdr["HeaderTag"].ToString() != "" ? sdr["HeaderTag"].ToString() : _productObj.HeaderTags);
                                        _productObj.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : _productObj.SupplierName);
                                        _productObj.CategoryID = (sdr["CategoryID"].ToString() != "" ? int.Parse(sdr["CategoryID"].ToString()) : _productObj.CategoryID);
                                        _productObj.PositionNo = (sdr["PositionNo"].ToString() != "" ? float.Parse(sdr["PositionNo"].ToString()) : _productObj.PositionNo);
                                        _productObj.LinkID = (sdr["LinkID"].ToString() != "" ? int.Parse(sdr["LinkID"].ToString()) : _productObj.LinkID);
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
            OperationsStatus operationsStatusObjH = null;
            OperationsStatus operationsStatusObjD = null;


            //------------transaction need to be put here ----------------------  
          //  _con = _databaseFactory.GetDBConnection();
          //  SqlTransaction transaction;
           // transaction = _con.BeginTransaction("InsertProduct");

            try
            {
                operationsStatusObjH = InsertProductHeader(productObj);
                if (operationsStatusObjH.StatusCode == 1)
                {
                      operationsStatusObjD = InsertUpdateProductDetails(productObj);
                    if (operationsStatusObjD.StatusCode == 0)
                    {
                        operationsStatusObjH.StatusCode = 0;
                        operationsStatusObjH.StatusMessage += operationsStatusObjD.StatusMessage;
                        //transaction.Rollback();
                    }
                    else if (operationsStatusObjD.StatusCode == 1)
                    {
                        operationsStatusObjH.StatusCode = 1;
                      //  operationsStatusObjH.StatusMessage += operationsStatusObjD.StatusMessage;
                    //    transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                operationsStatusObjH.StatusCode = -1;
                operationsStatusObjH.StatusMessage = e.Message;
                operationsStatusObjH.Exception = e;
             //   transaction.Rollback();
            }
          return operationsStatusObjH;
        }       
        private OperationsStatus InsertProductHeader(Product productObj) {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                SqlParameter outparamID = null;
                SqlParameter outAttributeset = null;
               
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertProductHeader]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = productObj.Name;
                        cmd.Parameters.Add("@SKU", SqlDbType.NVarChar, 250).Value = productObj.SKU;
                        cmd.Parameters.Add("@EnableYN", SqlDbType.Bit).Value = productObj.Enabled;
                        cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 10).Value = productObj.Unit;
                        cmd.Parameters.Add("@TaxClass", SqlDbType.NVarChar, 20).Value = productObj.TaxClass;
                        cmd.Parameters.Add("@URL", SqlDbType.NVarChar, -1).Value = productObj.URL;
                        cmd.Parameters.Add("@ShowPriceYN", SqlDbType.Bit).Value = productObj.ShowPrice;
                        cmd.Parameters.Add("@ActionType", SqlDbType.NVarChar, 1).Value = productObj.ActionType;
                        cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = productObj.SupplierID;
                        if(productObj.ManufacturerID>0)
                        {
                            cmd.Parameters.Add("@ManufacturerID", SqlDbType.Int).Value = productObj.ManufacturerID;
                        }
                       
                        cmd.Parameters.Add("@BaseSellingPrice", SqlDbType.Decimal).Value = productObj.BaseSellingPrice;
                        cmd.Parameters.Add("@CostPrice", SqlDbType.Decimal).Value = productObj.CostPrice;
                        cmd.Parameters.Add("@ShortDescription", SqlDbType.VarChar, 250).Value = productObj.ShortDescription;
                        cmd.Parameters.Add("@LongDescription", SqlDbType.NVarChar, -1).Value = productObj.LongDescription;
                        cmd.Parameters.Add("@ProductType", SqlDbType.VarChar, 1).Value = productObj.ProductType;
                        cmd.Parameters.Add("@StockAvailableYN", SqlDbType.Bit).Value = productObj.StockAvailable;
                        cmd.Parameters.Add("@AttributeSetID", SqlDbType.Int).Value = productObj.AttributeSetID;
                        cmd.Parameters.Add("@FreeDeliveryYN", SqlDbType.Bit).Value = productObj.FreeDelivery;
                        cmd.Parameters.Add("@HeaderTag", SqlDbType.NVarChar, -1).Value = productObj.HeaderTags;
                        cmd.Parameters.Add("@StickerID", SqlDbType.UniqueIdentifier).Value = DBNull.Value; //productObj.StickerID;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = productObj.logDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = productObj.logDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        outparamID = cmd.Parameters.Add("@ProductID", SqlDbType.SmallInt);
                        outparamID.Direction = ParameterDirection.Output;
                        outAttributeset = cmd.Parameters.Add("@AttributeSetName", SqlDbType.NVarChar,50);
                        outAttributeset.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                productObj.ID = 0;
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                productObj.ID= int.Parse(outparamID.Value.ToString());
                                operationsStatusObj.ReturnValues = new {
                                    productid = productObj.ID,
                                    attributesetname = outAttributeset.Value.ToString()
                                };
                                
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
        public OperationsStatus InsertUpdateProductDetails(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                SqlParameter outparamID = null;
                bool IsinsertOrUpdate=false;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                   
                    foreach (ProductDetail detail in productObj.ProductDetails)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            if (detail.ID == 0)
                            {
                                cmd.CommandText = "[InsertProductDetails]";
                                IsinsertOrUpdate = true;
                                //detail.DefaultOption = true;
                            }
                            else
                            {
                                cmd.CommandText = "[UpdateProductDetails]";
                            }

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = detail.ID;//for insert it will be 0;
                            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productObj.ID;
                            cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = detail.Qty;
                            cmd.Parameters.Add("@OutOfStockAlertQty", SqlDbType.Int).Value = detail.OutOfStockAlertQty;
                            cmd.Parameters.Add("@PriceDiffAmt", SqlDbType.Decimal).Value = detail.PriceDifference;
                            cmd.Parameters.Add("@AttributeXML", SqlDbType.Xml).Value = _attributesRepository.GetAttributeXML(detail.ProductAttributes);
                            cmd.Parameters.Add("@DetailTag", SqlDbType.NVarChar, -1).Value = detail.DetailTags;
                            cmd.Parameters.Add("@EnableYN", SqlDbType.Bit).Value = detail.Enabled;
                            cmd.Parameters.Add("@StockAvailableYN", SqlDbType.Bit).Value = detail.StockAvailable;
                            cmd.Parameters.Add("@DiscountAmout", SqlDbType.Decimal).Value = detail.DiscountAmount;
                            cmd.Parameters.Add("@DiscountStDate", SqlDbType.DateTime).Value = detail.DiscountStartDate;
                            cmd.Parameters.Add("@DiscountEnDate", SqlDbType.DateTime).Value = detail.DiscountEndDate;
                            cmd.Parameters.Add("@DefaultOptionYN", SqlDbType.Bit).Value = detail.DefaultOption;
                            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = productObj.logDetails.CreatedBy;
                            cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = productObj.logDetails.CreatedDate;
                            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = productObj.logDetails.UpdatedBy;
                            cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = productObj.logDetails.UpdatedDate;
                            statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                            statusCode.Direction = ParameterDirection.Output;
                            outparamID = cmd.Parameters.Add("@DetailID", SqlDbType.SmallInt);
                            outparamID.Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            operationsStatusObj = new OperationsStatus();
                            switch (statusCode.Value.ToString())
                            {
                            case "0":
                                    // not Successfull                                
                                    operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                    switch(IsinsertOrUpdate)
                                    {
                                        case true:
                                            operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                            break;
                                        case false:
                                            operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                            break;
                                    }
                                    
                                    return operationsStatusObj;
                                case "1":
                                    //InsertOrU Successfull
                                    operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                    switch(IsinsertOrUpdate)
                                    {
                                        case true:
                                            operationsStatusObj.StatusMessage = constObj.InsertSuccess;
                                            break;

                                        case false:
                                            operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                            break;
                                    }
                                    
                                    detail.ID = int.Parse(outparamID.Value.ToString());
                                    operationsStatusObj.ReturnValues = detail.ID;
                                return operationsStatusObj;
                            default:
                                break;
                            }
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
        public OperationsStatus UpdateProductSticker(Product productObj)
        {

            OperationsStatus operationsStatusObj = null;
            SqlParameter statusCode = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateProductSticker]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (productObj.StickerID != Guid.Empty&& productObj.StickerID!=null)
                        {
                            cmd.Parameters.Add("@StickerID", SqlDbType.UniqueIdentifier).Value = productObj.StickerID;
                        }
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productObj.ID;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull   
                                if (productObj.StickerID != Guid.Empty && productObj.StickerID != null)
                                {
                                    operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                    operationsStatusObj.StatusMessage = "Sticker Updation Not Successfull!";
                                }
                                else
                                {
                                    operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                    operationsStatusObj.StatusMessage = "Sticker Deletion Not Successfull!";
                                }
                                    
                                break;
                            case "1":
                                //Update Successfull
                                if (productObj.StickerID != Guid.Empty && productObj.StickerID != null)
                                {
                                    operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                    operationsStatusObj.StatusMessage = "Sticker Updation Successfull!";
                                }
                                 else
                                {
                                    operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                    operationsStatusObj.StatusMessage = "Sticker Deletion Successfull!";
                                }   
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
        public OperationsStatus UpdateProduct(Product productObj) {

            OperationsStatus operationsStatusObjH = null;
            OperationsStatus operationsStatusObjD = null;
            //------------transaction need to be put here ----------------------
            //_con = _databaseFactory.GetDBConnection();
           // SqlTransaction transaction;
            //transaction = _con.BeginTransaction("UpdateProduct");
            try
            {
                operationsStatusObjH = UpdateProductHeader(productObj);
                if (operationsStatusObjH.StatusCode == 1)
                {
                    operationsStatusObjD = InsertUpdateProductDetails(productObj);
                    if (operationsStatusObjD.StatusCode == 0)
                    {
                        operationsStatusObjH.StatusCode = 0;
                        operationsStatusObjH.StatusMessage += operationsStatusObjD.StatusMessage;
                       // transaction.Rollback();
                    }
                    else if (operationsStatusObjD.StatusCode == 1)
                    {
                        operationsStatusObjH.StatusCode = 1;
                        //operationsStatusObjH.StatusMessage += operationsStatusObjD.StatusMessage;
                        //transaction.Commit();
                    }
                }
                else
                {

                    //transaction.Rollback();
                }


                //---------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                //  transaction.Rollback();
                throw ex; 
            }
            return operationsStatusObjH;
        }
        private OperationsStatus UpdateProductHeader(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                SqlParameter outAttributeset = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateProductHeader]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productObj.ID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = productObj.Name;
                        cmd.Parameters.Add("@SKU", SqlDbType.NVarChar, 250).Value = productObj.SKU;
                        cmd.Parameters.Add("@EnableYN", SqlDbType.Bit).Value = productObj.Enabled;
                        cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 10).Value = productObj.Unit;
                        cmd.Parameters.Add("@TaxClass", SqlDbType.NVarChar, 20).Value = productObj.TaxClass;
                        cmd.Parameters.Add("@URL", SqlDbType.NVarChar, -1).Value = productObj.URL;
                        cmd.Parameters.Add("@ShowPriceYN", SqlDbType.Bit).Value = productObj.ShowPrice;
                        cmd.Parameters.Add("@ActionType", SqlDbType.NVarChar, 1).Value = productObj.ActionType;
                        cmd.Parameters.Add("@SupplierID", SqlDbType.Int).Value = productObj.SupplierID;
                        if(productObj.ManufacturerID>0)
                        {
                            cmd.Parameters.Add("@ManufacturerID", SqlDbType.Int).Value = productObj.ManufacturerID;
                        }
                        
                        cmd.Parameters.Add("@BaseSellingPrice", SqlDbType.Decimal).Value = productObj.BaseSellingPrice;
                        cmd.Parameters.Add("@CostPrice", SqlDbType.Decimal).Value = productObj.CostPrice;
                        cmd.Parameters.Add("@ShortDescription", SqlDbType.VarChar, 250).Value = productObj.ShortDescription;
                        cmd.Parameters.Add("@LongDescription", SqlDbType.NVarChar, -1).Value = productObj.LongDescription;
                        //cannot edit type- cmd.Parameters.Add("@ProductType", SqlDbType.VarChar, 1).Value = productObj.ProductType;
                        cmd.Parameters.Add("@StockAvailableYN", SqlDbType.Bit).Value = productObj.StockAvailable;
                        cmd.Parameters.Add("@AttributeSetID", SqlDbType.Int).Value = productObj.AttributeSetID;
                        cmd.Parameters.Add("@FreeDeliveryYN", SqlDbType.Bit).Value = productObj.FreeDelivery;
                        cmd.Parameters.Add("@HeaderTag", SqlDbType.NVarChar, -1).Value = productObj.HeaderTags;
                        cmd.Parameters.Add("@StickerID", SqlDbType.UniqueIdentifier).Value = DBNull.Value; ;//productObj.StickerID;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = productObj.logDetails.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = productObj.logDetails.UpdatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        outAttributeset= cmd.Parameters.Add("@AttributeSetName", SqlDbType.NVarChar, 50);
                        outAttributeset.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "updation Not Successfull!";
                                operationsStatusObj.ReturnValues = productObj.ID;
                                break;
                            case "1":
                                //update Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "updation Successfull!";
                                operationsStatusObj.ReturnValues = new
                                {
                                    productid = productObj.ID,
                                    attributesetname = outAttributeset.Value.ToString()
                                };
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
        private bool InsertProductImage(List<ProductImages> ImageList, LogDetails LogDetails,int ProductID,int DetailID=-1)
        {
            bool result= false;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    foreach (ProductImages image in ImageList)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandText = "[InsertProductImage]";
                            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                            cmd.Parameters.Add("@DetailID", SqlDbType.Int).Value = DetailID;
                            cmd.Parameters.Add("@URL", SqlDbType.NVarChar, -1).Value = image.URL;
                            cmd.Parameters.Add("@isMain", SqlDbType.Bit).Value = image.isMain;
                            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = LogDetails.CreatedBy;
                            cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = LogDetails.CreatedDate;
                        

                            cmd.ExecuteNonQuery();

                        }
                    }

                    result = true;


                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public OperationsStatus DeleteProductImage(int ID, int ProductID, int DetailID = -1)
        {
            OperationsStatus operationsStatusObj = null;
            SqlParameter statusCode = null;
            try
            {
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[DeleteProductImage]";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                        cmd.Parameters.Add("@DetailID", SqlDbType.Int).Value = DetailID;

                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return operationsStatusObj;
        }

        //delete product
        //delete product detail
        //Add related Products
        //Get related Products
        //Remove related Products

        public Product GetProduct(int ProductID, OperationsStatus Status)
        {
            Product myProduct = null;
            try
            {
                

                    myProduct = GetProductHeader(ProductID);
                    myProduct.ProductDetails = GetProductDetail(ProductID);
              
            }
            catch (Exception e)
            {
                Status.StatusCode = -1;
                Status.StatusMessage = e.Message;
                Status.Exception = e;

            }


            return myProduct;
        }
        public List<Product> GetRelatedImages(int ProductID, OperationsStatus Status)
        {
            List<Product> ProductList= null;
            try
            {
                using (_con = _databaseFactory.GetDBConnection())
                {
                    if (_con.State == ConnectionState.Closed)
                    {
                        _con.Open();
                    }
                    _cmd = new SqlCommand();
                    _cmd.Connection = _con;
                    _cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                    _cmd.CommandText = "[GetAllProductImages]";
                    _cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sdr = _cmd.ExecuteReader())
                    {
                        if ((sdr != null) && (sdr.HasRows))
                        {
                            ProductList = new List<Product>();
                            while (sdr.Read())
                            {
                                Product myProduct = new Product();
                                myProduct.ImageID = sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myProduct.ImageID;
                                myProduct.ImageURL = sdr["ImageURL"].ToString();
                                myProduct.ID = sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : ProductID; //sdr["Qty"].ToString();
                                myProduct.ProductDetID = sdr["ProductDetID"].ToString() != "" ? int.Parse(sdr["ProductDetID"].ToString()) : myProduct.ProductDetID;
                                myProduct.MainImage = sdr["MainImageYN"].ToString() != "" ? bool.Parse(sdr["MainImageYN"].ToString()) : myProduct.MainImage;
                                ProductList.Add(myProduct);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;

            }

            return ProductList;
        }
        public Product GetProductHeader(int ProductID)
        {
            Product myProduct = null;
            try
            {
                using (_con = _databaseFactory.GetDBConnection())
                {
                    if (_con.State == ConnectionState.Closed)
                    {
                        _con.Open();
                    }
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
                                myProduct.ID = sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myProduct.ID;
                                myProduct.Name = sdr["Name"].ToString();
                                myProduct.SKU = sdr["SKU"].ToString();
                                myProduct.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : myProduct.Enabled);
                                myProduct.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : myProduct.Unit);
                                myProduct.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr["TaxClass"].ToString() : myProduct.TaxClass);
                                myProduct.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : myProduct.URL);
                                myProduct.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : myProduct.ShowPrice);
                                myProduct.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : myProduct.ActionType);
                                myProduct.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : myProduct.SupplierID);
                                myProduct.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? int.Parse(sdr["ManufacturerID"].ToString()) : myProduct.ManufacturerID);
                                myProduct.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : myProduct.SupplierName);
                                myProduct.ManufacturerName = (sdr["ManufacturerName"].ToString() != "" ? sdr["ManufacturerName"].ToString() : myProduct.ManufacturerName);
                                myProduct.BaseSellingPrice = sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : myProduct.BaseSellingPrice;
                                myProduct.CostPrice = sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : myProduct.CostPrice;
                                myProduct.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : myProduct.ShortDescription);
                                myProduct.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : myProduct.LongDescription);
                                myProduct.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : myProduct.ProductType);
                                myProduct.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : myProduct.StockAvailable);
                                myProduct.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : myProduct.AttributeSetID;
                                myProduct.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : myProduct.FreeDelivery);
                                myProduct.HeaderTags = (sdr["HeaderTag"].ToString() != "" ? sdr["HeaderTag"].ToString() : myProduct.HeaderTags);
                                myProduct.StickerURL = (sdr["StickerURL"].ToString() != "" ? sdr["StickerURL"].ToString() : myProduct.StickerURL);
                                myProduct.StickerID = (sdr["StickerID"].ToString() != "" ? Guid.Parse(sdr["StickerID"].ToString()) : myProduct.StickerID); //sdr["StickerID"].ToString();
                                myProduct.AttributeSetName= (sdr["AttributeSetName"].ToString() != "" ? sdr["AttributeSetName"].ToString() : myProduct.AttributeSetName);
                                myProduct.ProductHeaderImages = GetProductImages(myProduct.ID);
                                
                                //myProduct.logDetails.CreatedBy = sdr["CreatedBy"].ToString();
                                //myProduct.logDetails.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["CreatedDate"].ToString())) : myProduct.logDetails.CreatedDate);
                                //myProduct.logDetails.UpdatedBy = sdr["UpdatedBy"].ToString();
                                //myProduct.logDetails.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["UpdatedDate"].ToString())) : myProduct.logDetails.UpdatedDate);

                                myProduct.ProductOtherAttributesXML = (sdr["OtherAttributeXML"].ToString() != "" ? sdr["OtherAttributeXML"].ToString() : myProduct.ProductOtherAttributesXML);
                                myProduct.OrderAttributes = _attributesRepository.GetAttributeContainer(myProduct.AttributeSetID, "Order", false);
                                myProduct.RatingAttributes = _attributesRepository.GetAttributeContainer(myProduct.AttributeSetID, "Rating", false);
                                if (!string.IsNullOrEmpty(myProduct.ProductOtherAttributesXML))
                                {
                                    myProduct.ProductOtherAttributes = _attributesRepository.GetAttributeFromXML(myProduct.ProductOtherAttributesXML, myProduct.AttributeSetID, "Product", false);
                                    myProduct.ProductOtherAttributes = myProduct.ProductOtherAttributes.FindAll(n => n.Isconfigurable == false);
                                }
                                else {
                                    myProduct.ProductOtherAttributes = _attributesRepository.GetAttributeContainer(myProduct.AttributeSetID, "Product");
                                }
                            }

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
        public List<ProductDetail> GetProductDetail(int ProductID)
        {
            List<ProductDetail> myProductDetails = null;
           // AttributesRepository myAttributesRepository = new AttributesRepository(_databaseFactory);
            List<AttributeValues> myAttributeStructure = null;
            try
            {
                using (_con = _databaseFactory.GetDBConnection())
                {
                    if (_con.State == ConnectionState.Closed)
                    {
                        _con.Open();
                    }
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
                                myProductDetail.ID = sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myProductDetail.ID;
                                myProductDetail.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : myProductDetail.AttributeSetID;
                                myProductDetail.Qty = sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : myProductDetail.Qty; //sdr["Qty"].ToString();
                                myProductDetail.OutOfStockAlertQty = sdr["OutOfStockAlertQty"].ToString() != "" ? int.Parse(sdr["OutOfStockAlertQty"].ToString()) : myProductDetail.OutOfStockAlertQty;
                                myProductDetail.PriceDifference = sdr["PriceDiffAmt"].ToString() != "" ? decimal.Parse(sdr["PriceDiffAmt"].ToString()) : myProductDetail.PriceDifference;
                                myProductDetail.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : myProductDetail.Enabled);
                                myProductDetail.DetailTags = (sdr["DetailTag"].ToString() != "" ? sdr["DetailTag"].ToString() : myProductDetail.DetailTags);
                                myProductDetail.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : myProductDetail.StockAvailable);
                                myProductDetail.DiscountAmount = sdr["DiscountAmout"].ToString() != "" ? decimal.Parse(sdr["DiscountAmout"].ToString()) : myProductDetail.DiscountAmount;
                                myProductDetail.DiscountStartDate = (sdr["DiscountStDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountStDate"].ToString())) : myProductDetail.DiscountStartDate);
                                myProductDetail.DiscountEndDate = (sdr["DiscountEnDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountEnDate"].ToString())) : myProductDetail.DiscountEndDate);
                                myProductDetail.ProductName=(sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : myProductDetail.ProductName);
                                myProductDetail.BaseSellingPrice= sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : myProductDetail.BaseSellingPrice;
                                myProductDetail.ActualPrice = (sdr["ActualPrice"].ToString() != "" ? decimal.Parse(sdr["ActualPrice"].ToString()) : myProductDetail.ActualPrice);
                                myProductDetail.DefaultOption = (sdr["DefaultOptionYN"].ToString() != "" ? bool.Parse(sdr["DefaultOptionYN"].ToString()) : myProductDetail.DefaultOption);
                                myProductDetail.logDetails = new LogDetails();
                                myProductDetail.logDetails.CreatedBy = sdr["CreatedBy"].ToString();
                                myProductDetail.logDetails.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? (DateTime.Parse(sdr["CreatedDate"].ToString())) : myProductDetail.logDetails.CreatedDate);
                                //myProductDetail.logDetails.UpdatedBy = sdr["UpdatedBy"].ToString();
                                //myProductDetail.logDetails.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["UpdatedDate"].ToString())) : myProductDetail.logDetails.UpdatedDate);

                                if (myAttributeStructure == null)
                                {
                                    myAttributeStructure = _attributesRepository.GetAttributeContainer(myProductDetail.AttributeSetID, "Product",true);
                                }

                                myProductDetail.ProductAttributes = new List<AttributeValues>();
                                foreach (AttributeValues att in myAttributeStructure)
                                {
                                   
                                    AttributeValues myAttribute = new AttributeValues(att);//copy the values
                                    try
                                    {
                                        //Checks column exists in reader
                                        for (int i = 0; i < sdr.FieldCount; i++)
                                        {
                                            if (sdr.GetName(i).Equals(att.Caption.ToString(), StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                myAttribute.Value = sdr[att.Caption].ToString();
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                    myProductDetail.ProductAttributes.Add(myAttribute);
                                   
                                }
                                myProductDetails.Add(myProductDetail);
                                myProductDetail.ProductDetailImages = GetProductImages(myProductDetail.ProductID, myProductDetail.ID);

                            }
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
        public List<ProductDetail> GetAllProductDetail()
        {
            List<ProductDetail> myProductDetails = null;
            try
            {
                using (_con = _databaseFactory.GetDBConnection())
                {
                    if (_con.State == ConnectionState.Closed)
                    {
                        _con.Open();
                    }
                    _cmd = new SqlCommand();
                    _cmd.Connection = _con;
                    _cmd.CommandText = "[GetAllProductDetail]";
                    _cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sdr = _cmd.ExecuteReader())
                    {
                        if ((sdr != null) && (sdr.HasRows))
                        {
                            myProductDetails = new List<ProductDetail>();
                            while (sdr.Read())
                            {
                                ProductDetail myProductDetail = new ProductDetail();
                                myProductDetail.ProductID = sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : myProductDetail.ProductID;
                                myProductDetail.ID = sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myProductDetail.ID;
                                //myProductDetail.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : myProductDetail.AttributeSetID;
                                myProductDetail.Qty = sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : myProductDetail.Qty; //sdr["Qty"].ToString();
                                //myProductDetail.OutOfStockAlertQty = sdr["OutOfStockAlertQty"].ToString() != "" ? int.Parse(sdr["OutOfStockAlertQty"].ToString()) : myProductDetail.OutOfStockAlertQty;
                                myProductDetail.PriceDifference = sdr["PriceDiffAmt"].ToString() != "" ? decimal.Parse(sdr["PriceDiffAmt"].ToString()) : myProductDetail.PriceDifference;
                                //myProductDetail.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : myProductDetail.Enabled);
                                //myProductDetail.DetailTags = (sdr["DetailTag"].ToString() != "" ? sdr["DetailTag"].ToString() : myProductDetail.DetailTags);
                                //myProductDetail.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : myProductDetail.StockAvailable);
                                myProductDetail.DiscountAmount = sdr["DiscountAmout"].ToString() != "" ? decimal.Parse(sdr["DiscountAmout"].ToString()) : myProductDetail.DiscountAmount;
                                //myProductDetail.DiscountStartDate = (sdr["DiscountStDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountStDate"].ToString())) : myProductDetail.DiscountStartDate);
                                myProductDetail.DiscountEndDate = (sdr["DiscountEnDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountEnDate"].ToString())) : myProductDetail.DiscountEndDate);
                                myProductDetail.ProductName = (sdr["Name"].ToString() != "" ? (sdr["Name"].ToString() + "||" + sdr["AttributeXML"].ToString()) : myProductDetail.ProductName);
                                myProductDetail.BaseSellingPrice = sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : myProductDetail.BaseSellingPrice;
                                myProductDetail.ActualPrice = (sdr["ActualPrice"].ToString() != "" ? decimal.Parse(sdr["ActualPrice"].ToString()) : myProductDetail.ActualPrice);
                                //myProductDetail.DefaultOption = (sdr["DefaultOptionYN"].ToString() != "" ? bool.Parse(sdr["DefaultOptionYN"].ToString()) : myProductDetail.DefaultOption);
                                //myProductDetail.logDetails = new LogDetails();
                                //myProductDetail.logDetails.CreatedBy = sdr["CreatedBy"].ToString();
                                //myProductDetail.logDetails.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? (DateTime.Parse(sdr["CreatedDate"].ToString())) : myProductDetail.logDetails.CreatedDate);
                                //myProductDetail.logDetails.UpdatedBy = sdr["UpdatedBy"].ToString();
                                //myProductDetail.logDetails.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["UpdatedDate"].ToString())) : myProductDetail.logDetails.UpdatedDate);

                                //    if (myAttributeStructure == null)
                                //    {
                                //        myAttributeStructure = _attributesRepository.GetAttributeContainer(myProductDetail.AttributeSetID, "Product", true);
                                //    }

                                //    myProductDetail.ProductAttributes = new List<AttributeValues>();
                                //    foreach (AttributeValues att in myAttributeStructure)
                                //    {

                                //        AttributeValues myAttribute = new AttributeValues(att);//copy the values
                                //        try
                                //        {
                                //            //Checks column exists in reader
                                //            for (int i = 0; i < sdr.FieldCount; i++)
                                //            {
                                //                if (sdr.GetName(i).Equals(att.Caption.ToString(), StringComparison.InvariantCultureIgnoreCase))
                                //                {
                                //                    myAttribute.Value = sdr[att.Caption].ToString();
                                //                    break;
                                //                }
                                //            }
                                //        }
                                //        catch (Exception ex)
                                //        {
                                //        }
                                //        myProductDetail.ProductAttributes.Add(myAttribute);

                                //    }
                                   myProductDetails.Add(myProductDetail);
                                //    myProductDetail.ProductDetailImages = GetProductImages(myProductDetail.ProductID, myProductDetail.ID);

                                //}
                            }
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

        private List<ProductImages> GetProductImages(int ProductID)
        {
            List<ProductImages> myImages = null;
            try
            {
                myImages = GetPrdImg(ProductID, -1);
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
                myImages = GetPrdImg(ProductID, ProductDetailID);
            }
            catch (Exception e)
            {

                throw e;
            }
            return myImages;

        }
        private List<ProductImages> GetPrdImg(int ProductID, int ProductDetailID = -1)
        {
            List<ProductImages> myImagesList = null;
            SqlConnection myCon;
            try
            {

                using (myCon = _databaseFactory.GetDBConnection())
                {
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
                                myImage.ID = (int)(sdr["ID"]);
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
        public List<Product> GetRelatedProducts(int productID)
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
                        cmd.CommandText = "[GetRelatedProductsByProduct]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productID;


                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                productList = new List<Product>();
                                while (sdr.Read())
                                {
                                    Product _productObj = new Product();
                                    {
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.SKU = (sdr["SKU"].ToString() != "" ? sdr["SKU"].ToString() : _productObj.SKU);
                                        _productObj.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : _productObj.Enabled);
                                        _productObj.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : _productObj.Unit);
                                        _productObj.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr[""].ToString() : _productObj.TaxClass);
                                        _productObj.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : _productObj.URL);
                                        _productObj.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : _productObj.ShowPrice);
                                        _productObj.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : _productObj.ActionType);
                                        _productObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : _productObj.SupplierID);
                                        _productObj.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? int.Parse(sdr["ManufacturerID"].ToString()) : _productObj.ManufacturerID);
                                        _productObj.BaseSellingPrice = (sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : _productObj.BaseSellingPrice);
                                        _productObj.CostPrice = (sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : _productObj.CostPrice);
                                        _productObj.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : _productObj.ShortDescription);
                                        _productObj.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : _productObj.LongDescription);
                                        _productObj.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : _productObj.ProductType);
                                        _productObj.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : _productObj.StockAvailable);
                                        _productObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : _productObj.AttributeSetID);
                                        _productObj.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : _productObj.FreeDelivery);
                                        _productObj.HeaderTags = (sdr["HeaderTag"].ToString() != "" ? sdr["HeaderTag"].ToString() : _productObj.HeaderTags);
                                        _productObj.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : _productObj.SupplierName);
                                        _productObj.ManufacturerName = (sdr["ManufacturerName"].ToString() != "" ? sdr["ManufacturerName"].ToString() : _productObj.ManufacturerName);
                                        _productObj.StickerURL = (sdr["StickerURL"].ToString() != "" ? sdr["StickerURL"].ToString() : _productObj.StickerURL);
                                        _productObj.ImageURL= (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _productObj.ImageURL);
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
        public List<Product> GetUNRelatedProducts(int productID)
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
                        cmd.CommandText = "[GetUNRelatedProductsByProduct]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productID;


                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                productList = new List<Product>();
                                while (sdr.Read())
                                {
                                    Product _productObj = new Product();
                                    {
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.SKU = (sdr["SKU"].ToString() != "" ? sdr["SKU"].ToString() : _productObj.SKU);
                                        _productObj.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : _productObj.Enabled);
                                        _productObj.Unit = (sdr["Unit"].ToString() != "" ? sdr["Unit"].ToString() : _productObj.Unit);
                                        _productObj.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr[""].ToString() : _productObj.TaxClass);
                                        _productObj.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : _productObj.URL);
                                        _productObj.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : _productObj.ShowPrice);
                                        _productObj.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : _productObj.ActionType);
                                        _productObj.SupplierID = (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : _productObj.SupplierID);
                                        _productObj.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? int.Parse(sdr["ManufacturerID"].ToString()) : _productObj.ManufacturerID);
                                        _productObj.BaseSellingPrice = (sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : _productObj.BaseSellingPrice);
                                        _productObj.CostPrice = (sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : _productObj.CostPrice);
                                        _productObj.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : _productObj.ShortDescription);
                                        _productObj.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : _productObj.LongDescription);
                                        _productObj.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : _productObj.ProductType);
                                        _productObj.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : _productObj.StockAvailable);
                                        _productObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : _productObj.AttributeSetID);
                                        _productObj.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : _productObj.FreeDelivery);
                                        _productObj.HeaderTags = (sdr["HeaderTag"].ToString() != "" ? sdr["HeaderTag"].ToString() : _productObj.HeaderTags);
                                        _productObj.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : _productObj.SupplierName);
                                        _productObj.ManufacturerName = (sdr["ManufacturerName"].ToString() != "" ? sdr["ManufacturerName"].ToString() : _productObj.ManufacturerName);
                                        _productObj.StickerURL = (sdr["StickerURL"].ToString() != "" ? sdr["StickerURL"].ToString() : _productObj.StickerURL);

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
        public OperationsStatus AddOrRemoveProductCategoryLink(List<ProductCategoryLink> AddList, List<ProductCategoryLink> DeleteList)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                foreach (ProductCategoryLink i in AddList)
                {
                    InsertProductCategoryLink(i);
                }
                foreach (ProductCategoryLink i in DeleteList)
                {
                    DeleteProductCategoryLink(i);
                }
                operationsStatusObj = new OperationsStatus();
                operationsStatusObj.StatusCode = Int16.Parse("1");
                operationsStatusObj.StatusMessage = "Changes Reflected Successfully!";

            }
            catch (Exception ex)
            {

                operationsStatusObj.StatusMessage = ex.Message;
            }

            return operationsStatusObj;

        }
        private OperationsStatus InsertProductCategoryLink(ProductCategoryLink productCategoryLinkObj)
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
                        cmd.CommandText = "[InsertProductCategoryLink]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productCategoryLinkObj.ProductID;
                        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = productCategoryLinkObj.CategoryID;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = productCategoryLinkObj.commonObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = productCategoryLinkObj.commonObj.CreatedDate;
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
        private OperationsStatus DeleteProductCategoryLink(ProductCategoryLink productCategoryLinkObj)
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
                        cmd.CommandText = "[DeleteProductCategoryLink]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productCategoryLinkObj.ID;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // Delete not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Deletion Not Successfull!";
                                break;
                            case "1":
                                //Delete Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = "Deletion Successfull!";
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
        public OperationsStatus InsertRelatedProducts(Product productObj, string IDList)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                    con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertRelatedProducts]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productObj.ID;
                        cmd.Parameters.Add("@RelatedProdcutIDCollection", SqlDbType.NVarChar, -1).Value = IDList;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = productObj.logDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = productObj.logDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Not Successfull!";
                                return operationsStatusObj;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                return operationsStatusObj;
                            default:
                                break;
                        }
                   }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return operationsStatusObj;

        }
        public OperationsStatus DeleteRelatedProducts(Product productObj, string IDList)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[DeleteRelatedProducts]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productObj.ID;
                        cmd.Parameters.Add("@RelatedProdcutIDCollection", SqlDbType.NVarChar, -1).Value = IDList;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteFailure;
                                return operationsStatusObj;
                            case "1":
                                //Delete Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteSuccess;
                                return operationsStatusObj;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return operationsStatusObj;

        }
        public OperationsStatus UpdateProductHeaderOtherAttributes(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateProductHeaderOtherAttributes]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productObj.ID;
                        cmd.Parameters.Add("@OtherAttributeXML", SqlDbType.Xml).Value = _attributesRepository.GetAttributeXML(productObj.ProductOtherAttributes);
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                return operationsStatusObj;
                            case "1":
                               
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                return operationsStatusObj;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return operationsStatusObj;

        }
        public ProductDetail GetProductDetailsByProduct(int ProductID,int DetailID)
        {
           ProductDetail myProductDetail = null;
            // AttributesRepository myAttributesRepository = new AttributesRepository(_databaseFactory);
            List<AttributeValues> myAttributeStructure = null;
            try
            {
                using (_con = _databaseFactory.GetDBConnection())
                {
                    if (_con.State == ConnectionState.Closed)
                    {
                        _con.Open();
                    }
                    _cmd = new SqlCommand();
                    _cmd.Connection = _con;
                    _cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                    _cmd.Parameters.Add("@DetailID", SqlDbType.Int).Value = DetailID;
                    _cmd.CommandText = "[GetProductDetailsByDetailID]";
                    _cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sdr = _cmd.ExecuteReader())
                    {
                        if ((sdr != null) && (sdr.HasRows))
                        {
                          if(sdr.Read())
                            {
                                myProductDetail = new ProductDetail();
                                myProductDetail.ProductID = ProductID;
                                myProductDetail.ID = sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myProductDetail.ID;
                                myProductDetail.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : myProductDetail.AttributeSetID;
                                myProductDetail.Qty = sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : myProductDetail.Qty; //sdr["Qty"].ToString();
                                myProductDetail.OutOfStockAlertQty = sdr["OutOfStockAlertQty"].ToString() != "" ? int.Parse(sdr["OutOfStockAlertQty"].ToString()) : myProductDetail.OutOfStockAlertQty;
                                myProductDetail.PriceDifference = sdr["PriceDiffAmt"].ToString() != "" ? decimal.Parse(sdr["PriceDiffAmt"].ToString()) : myProductDetail.PriceDifference;
                                myProductDetail.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : myProductDetail.Enabled);
                                myProductDetail.DetailTags = (sdr["DetailTag"].ToString() != "" ? sdr["DetailTag"].ToString() : myProductDetail.DetailTags);
                                myProductDetail.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : myProductDetail.StockAvailable);
                                myProductDetail.DiscountAmount = sdr["DiscountAmout"].ToString() != "" ? decimal.Parse(sdr["DiscountAmout"].ToString()) : myProductDetail.DiscountAmount;
                                myProductDetail.DiscountStartDate = (sdr["DiscountStDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountStDate"].ToString())) : myProductDetail.DiscountStartDate);
                                myProductDetail.DiscountEndDate = (sdr["DiscountEnDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountEnDate"].ToString())) : myProductDetail.DiscountEndDate);
                                myProductDetail.ProductName = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : myProductDetail.ProductName);
                                myProductDetail.DefaultOption= (sdr["DefaultOptionYN"].ToString() != "" ? bool.Parse(sdr["DefaultOptionYN"].ToString()) : myProductDetail.DefaultOption);
                                myProductDetail.BaseSellingPrice = sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : myProductDetail.BaseSellingPrice;
                                //myProductDetail.logDetails.CreatedBy = sdr["CreatedBy"].ToString();
                                //myProductDetail.logDetails.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["CreatedDate"].ToString())) : myProductDetail.logDetails.CreatedDate);
                                //myProductDetail.logDetails.UpdatedBy = sdr["UpdatedBy"].ToString();
                                //myProductDetail.logDetails.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["UpdatedDate"].ToString())) : myProductDetail.logDetails.UpdatedDate);

                                if (myAttributeStructure == null)
                                {
                                    myAttributeStructure = _attributesRepository.GetAttributeContainer(myProductDetail.AttributeSetID, "Product");
                                }

                                myProductDetail.ProductAttributes = new List<AttributeValues>();
                                foreach (AttributeValues att in myAttributeStructure)
                                {

                                    AttributeValues myAttribute = new AttributeValues(att);//copy the values
                                    try
                                    {
                                        //Checks column exists in reader
                                        for (int i = 0; i < sdr.FieldCount; i++)
                                        {
                                            if (sdr.GetName(i).Equals(att.Caption.ToString(), StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                myAttribute.Value = sdr[att.Caption].ToString();
                                                break;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                    myProductDetail.ProductAttributes.Add(myAttribute);

                                }
                                
                                myProductDetail.ProductDetailImages = GetProductImages(myProductDetail.ProductID, myProductDetail.ID);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;

            }

            return myProductDetail;
        }
        public OperationsStatus DeleteProductsDetails(int ProductDetailsID,int ProductID)
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
                        cmd.CommandText = "[DeleteProductDetails]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ProductDetailsID;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // Delete not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteFailure;
                                break;
                            case "1":
                                //Delete Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteSuccess;
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
        public OperationsStatus DeleteProductImage(int ID)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter outparameter = null;
                SqlParameter OutparameterURL = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Connection = con;
                        cmd.CommandText = "[DeleteProductImage]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        OutparameterURL = cmd.Parameters.Add("@URL", SqlDbType.NVarChar, -1);
                        outparameter.Direction = ParameterDirection.Output;
                        OutparameterURL.Direction= ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                // Delete not Successfull

                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteFailure;
                                break;
                            case "1":
                                //Delete Successfull
                                if (outparameter.Value.ToString() == "1")
                                {
                                    try
                                    {
                                        if (OutparameterURL.Value.ToString() != "")
                                        {
                                            System.IO.File.Delete(HttpContext.Current.Server.MapPath(OutparameterURL.Value.ToString()));
                                        }


                                    }
                                    catch (System.IO.IOException e)
                                    {
                                        throw e;

                                    }
                                }
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteSuccess;
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
        public OperationsStatus InsertImageProduct(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[InsertProductImage]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productObj.ID;
                        if(productObj.ProductDetID!=0)
                        {
                            cmd.Parameters.Add("@DetailID", SqlDbType.NVarChar, -1).Value = productObj.ProductDetID;
                        }
                        
                        cmd.Parameters.Add("@URL", SqlDbType.NVarChar, -1).Value = productObj.ImageURL;
                        cmd.Parameters.Add("@isMain", SqlDbType.Bit).Value = productObj.MainImage;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = productObj.logDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = productObj.logDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Not Successfull!";
                                return operationsStatusObj;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                return operationsStatusObj;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return operationsStatusObj;
        }
        public List<ProductReview> GetProductReviews(int ProductID)
        {
            List<ProductReview> productReviewList = null;
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
                        cmd.CommandText = "[GetProductReviewsandRating]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;


                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                productReviewList = new List<ProductReview>();
                                while (sdr.Read())
                                {
                                    ProductReview _pReviewObj = new ProductReview();
                                    {
                                        _pReviewObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _pReviewObj.ID);
                                        _pReviewObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _pReviewObj.CustomerID);
                                        _pReviewObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _pReviewObj.ProductID);
                                        _pReviewObj.Review = (sdr["Review"].ToString() != "" ? sdr["Review"].ToString() : _pReviewObj.Review);
                                        _pReviewObj.ReviewCreatedDate = (sdr["ReviewCreatedDate"].ToString() != "" ? DateTime.Parse(sdr["ReviewCreatedDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _pReviewObj.ReviewCreatedDate);
                                        _pReviewObj.DaysCount = (sdr["DaysCount"].ToString() != "" ? int.Parse(sdr["DaysCount"].ToString()) : _pReviewObj.DaysCount);
                                        _pReviewObj.CustomerName = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _pReviewObj.CustomerName);
                                        _pReviewObj.AvgRating = (sdr["AvgRating"].ToString() != "" ? sdr["AvgRating"].ToString() : _pReviewObj.AvgRating);
                                        _pReviewObj.ImageUrl = (sdr["ImageUrl"].ToString() != "" ? sdr["ImageUrl"].ToString() : _pReviewObj.ImageUrl);
                                    }
                                    productReviewList.Add(_pReviewObj);
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

            return productReviewList;
        }
        public List<ProductReview> GetRatingSummary(int ProductID, int AttributesetId)
        {
            List<ProductReview> RatingSummary = null;
            List<AttributeValues> myAttributeStructure = null;
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
                        cmd.CommandText = "[GetProductRatingSummary]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;


                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                RatingSummary = new List<ProductReview>();
                                while (sdr.Read())
                                {
                                    ProductReview _pReviewObj = new ProductReview();
                                    {
                                        _pReviewObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _pReviewObj.ProductID);
                                        _pReviewObj.RatingCount = (sdr["RatingCount"].ToString() != "" ? sdr["RatingCount"].ToString() : _pReviewObj.RatingCount);

                                        if (myAttributeStructure == null)
                                        {
                                            myAttributeStructure = _attributesRepository.GetAttributeContainer(AttributesetId, "Rating");
                                        }

                                        _pReviewObj.ProductRatingAttributes = new List<AttributeValues>();
                                        foreach (AttributeValues att in myAttributeStructure)
                                        {
                                            att.Value = sdr[att.Caption].ToString();
                                            _pReviewObj.ProductRatingAttributes.Add(att);

                                        }
                                    }
                                    RatingSummary.Add(_pReviewObj);
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return RatingSummary;

        }

        #region For app
       
        public List<Product> GetTopProductsOfCategory(Categories categoryObj)
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
                        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryObj.ID;
                        cmd.CommandText = "[GetTopProductsOfCategoryForApp]";
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
                                        _productObj.ID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.ImageURL = (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _productObj.ImageURL);                                      
                                    }
                                    productList.Add(_productObj);
                                }
                            }
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

        public List<Product> GetProductsOfCategory(Categories categoryObj)
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
                        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = categoryObj.ID;
                        cmd.CommandText = "[GetProductsOfCategoryForApp]";
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
                                        _productObj.ID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.ImageURL = (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _productObj.ImageURL);
                                    }
                                    productList.Add(_productObj);
                                }
                            }
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

        public List<Product> GetProductsByFiltering(FilterCriteria filterCritiria)
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
                        cmd.Parameters.Add("@FilterCategories", SqlDbType.NVarChar,500).Value = filterCritiria.filterCriteriaCSV;
                        cmd.CommandText = "[GetProductsByFilteringForApp]";
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
                                        _productObj.ID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _productObj.ID);
                                        _productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _productObj.Name);
                                        _productObj.ImageURL = (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _productObj.ImageURL);
                                        _productObj.CategoryID = (sdr["CategoryID"].ToString() != "" ? int.Parse(sdr["CategoryID"].ToString()) : _productObj.CategoryID);                                        
                                    }
                                    productList.Add(_productObj);
                                }
                            }
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

        public Product GetProductDetailsForApp(int productID, DateTime currentDateTime,int customerID)
        {
            Product productObj=new Product();
            productObj.ID = productID;
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
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productObj.ID;
                        cmd.Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = currentDateTime.Date;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
                        cmd.CommandText = "[GetProductDetailsForApp]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                productObj.ProductDetailObj = new ProductDetail();
                                while (sdr.Read())
                                {                                    
                                    productObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : productObj.Name);
                                    productObj.SKU = (sdr["SKU"].ToString() != "" ? sdr["SKU"].ToString() : productObj.SKU);
                                    productObj.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) : productObj.ShowPrice);
                                    productObj.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : productObj.ActionType);
                                    productObj.SupplierID= (sdr["SupplierID"].ToString() != "" ? int.Parse(sdr["SupplierID"].ToString()) : productObj.SupplierID);
                                    productObj.SupplierName = (sdr["SupplierName"].ToString() != "" ? sdr["SupplierName"].ToString() : productObj.SupplierName);
                                    productObj.BaseSellingPrice = (sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : productObj.BaseSellingPrice);
                                    productObj.ShortDescription = (sdr["ShortDescription"].ToString() != "" ? sdr["ShortDescription"].ToString() : productObj.ShortDescription);
                                    productObj.LongDescription = (sdr["LongDescription"].ToString() != "" ? sdr["LongDescription"].ToString() : productObj.LongDescription);
                                    productObj.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : productObj.ProductType);
                                    productObj.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) : productObj.FreeDelivery);
                                    productObj.StickerURL = (sdr["StickerURL"].ToString() != "" ? sdr["StickerURL"].ToString() : productObj.StickerURL); 
                                    productObj.ProductDetailObj.PriceDifference = (sdr["PriceDiffAmt"].ToString() != "" ? decimal.Parse(sdr["PriceDiffAmt"].ToString()) : productObj.ProductDetailObj.PriceDifference);
                                    productObj.StockAvailable = (sdr["InStock"].ToString() != "" ? (sdr["InStock"].ToString()=="0"?false:true) : productObj.StockAvailable);
                                    productObj.ProductDetailObj.DiscountAmount = (sdr["DiscountAmount"].ToString() != "" ? decimal.Parse(sdr["DiscountAmount"].ToString()) : productObj.ProductDetailObj.DiscountAmount);
                                    productObj.AttributeSetID = (sdr["AttributeSetID"].ToString() != "" ? int.Parse(sdr["AttributeSetID"].ToString()) : productObj.AttributeSetID);
                                    productObj.IsFav= (sdr["IsFav"].ToString() != "" ? (sdr["IsFav"].ToString() == "0" ? false : true) : productObj.IsFav);

                                    productObj.ProductOtherAttributesXML = (sdr["OtherAttributeXML"].ToString() != "" ? sdr["OtherAttributeXML"].ToString() : productObj.ProductOtherAttributesXML);
                                    if (!string.IsNullOrEmpty(productObj.ProductOtherAttributesXML))
                                    {
                                        productObj.ProductOtherAttributes = _attributesRepository.GetAttributeFromXML(productObj.ProductOtherAttributesXML, productObj.AttributeSetID, "Product", false);
                                        productObj.ProductOtherAttributes = productObj.ProductOtherAttributes.FindAll(n => n.Isconfigurable == false);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return productObj;
        }

        public Product GetProductSticker(int productID)
        {
            Product productObj = new Product();
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
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
                       
                        cmd.CommandText = "[GetProductSticker]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                               
                                while (sdr.Read())
                                {
                                   
                                    productObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : productObj.ID);
                                    productObj.StickerURL = (sdr["StickerURL"].ToString() != "" ? sdr["StickerURL"].ToString() : productObj.StickerURL);
                                 
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return productObj;
        }

        public List<ProductImages> GetProductImagesforApp(int ProductID)
        {
            List<ProductImages> myImagesList = null;
            SqlConnection myCon;
            int ProductDetailID = -1;
            try
            {

                using (myCon = _databaseFactory.GetDBConnection())
                {
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
                                myImage.ID = (int)(sdr["ID"]);
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

        public OperationsStatus UpdateWishlist(Wishlist WishListObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "[UpdateWishList]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = WishListObj.ProductID;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = WishListObj.CustomerID; 
                        cmd.Parameters.Add("@ProductSpecXML", SqlDbType.NVarChar,-1).Value = WishListObj.ProductSpecXML;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = WishListObj.logDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = WishListObj.logDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":            
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                return operationsStatusObj;
                            case "1": 
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                return operationsStatusObj;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return operationsStatusObj;

        }

        #endregion
    }
}