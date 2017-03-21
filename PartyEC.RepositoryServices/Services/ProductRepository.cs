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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _productObj.ID);
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
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Int16.Parse(CategoryID.ToString());
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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _productObj.ID);
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
                                        _productObj.CategoryID = (sdr["CategoryID"].ToString() != "" ? Int16.Parse(sdr["CategoryID"].ToString()) : _productObj.CategoryID);
                                        _productObj.PositionNo = (sdr["PositionNo"].ToString() != "" ? float.Parse(sdr["PositionNo"].ToString()) : _productObj.PositionNo);
                                        _productObj.LinkID = (sdr["LinkID"].ToString() != "" ? Int16.Parse(sdr["LinkID"].ToString()) : _productObj.LinkID);
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
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Int16.Parse(CategoryID.ToString());
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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _productObj.ID);
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
                                        _productObj.CategoryID = (sdr["CategoryID"].ToString() != "" ? Int16.Parse(sdr["CategoryID"].ToString()) : _productObj.CategoryID);
                                        _productObj.PositionNo = (sdr["PositionNo"].ToString() != "" ? float.Parse(sdr["PositionNo"].ToString()) : _productObj.PositionNo);
                                        _productObj.LinkID = (sdr["LinkID"].ToString() != "" ? Int16.Parse(sdr["LinkID"].ToString()) : _productObj.LinkID);
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
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Int16.Parse(CategoryID.ToString());
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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _productObj.ID);
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
                                        _productObj.CategoryID = (sdr["CategoryID"].ToString() != "" ? Int16.Parse(sdr["CategoryID"].ToString()) : _productObj.CategoryID);
                                        _productObj.PositionNo = (sdr["PositionNo"].ToString() != "" ? float.Parse(sdr["PositionNo"].ToString()) : _productObj.PositionNo);
                                        _productObj.LinkID = (sdr["LinkID"].ToString() != "" ? Int16.Parse(sdr["LinkID"].ToString()) : _productObj.LinkID);
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
                        cmd.Parameters.Add("@ManufacturerID", SqlDbType.Int).Value = productObj.ManufacturerID;
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
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Insertion Successfull!";
                                productObj.ID= int.Parse(outparamID.Value.ToString());
                                operationsStatusObj.ReturnValues = productObj.ID;
                                break;
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
        public OperationsStatus InsertUpdateProductDetails(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                SqlParameter outparamID = null;

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
                            cmd.Parameters.Add("@DiscountStDate", SqlDbType.SmallDateTime).Value = detail.DiscountStartDate;
                            cmd.Parameters.Add("@DiscountEnDate", SqlDbType.SmallDateTime).Value = detail.DiscountEndDate;
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
                                    operationsStatusObj.StatusMessage = "Detail Insertion Not Successfull!";
                                    return operationsStatusObj;
                                case "1":
                                    //Insert Successfull
                                    operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                    operationsStatusObj.StatusMessage = "Detail Insertion Successfull!";
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {
              //  transaction.Rollback();

            }
            return operationsStatusObjH;
        }
        private OperationsStatus UpdateProductHeader(Product productObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
              
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
                        cmd.Parameters.Add("@ManufacturerID", SqlDbType.Int).Value = productObj.ManufacturerID;
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
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "updation Not Successfull!";
                                break;
                            case "1":
                                //Insert Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "updation Successfull!";

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
                using (_con = _databaseFactory.GetDBConnection())
                {
                    if (_con.State == ConnectionState.Closed)
                    {
                        _con.Open();
                    }

                    myProduct = GetProductHeader(ProductID);
                    myProduct.ProductDetails = GetProductDetail(ProductID);
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

        private Product GetProductHeader(int ProductID)
        {
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
                            myProduct.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : myProduct.Enabled);
                            myProduct.Unit = (sdr["Unit"].ToString()!=""? sdr["Unit"].ToString():myProduct.Unit);
                            myProduct.TaxClass = (sdr["TaxClass"].ToString() != "" ? sdr["TaxClass"].ToString() : myProduct.TaxClass);
                            myProduct.URL = (sdr["URL"].ToString() != "" ? sdr["URL"].ToString() : myProduct.URL);
                            myProduct.ShowPrice = (sdr["ShowPriceYN"].ToString() != "" ? bool.Parse(sdr["ShowPriceYN"].ToString()) :myProduct.ShowPrice);
                            myProduct.ActionType = (sdr["ActionType"].ToString() != "" ? char.Parse(sdr["ActionType"].ToString()) : myProduct.ActionType);
                            myProduct.SupplierID = (sdr["SupplierID"].ToString() != "" ? Int16.Parse(sdr["SupplierID"].ToString()) : myProduct.SupplierID);
                            myProduct.ManufacturerID = (sdr["ManufacturerID"].ToString() != "" ? Int16.Parse(sdr["ManufacturerID"].ToString()) : myProduct.ManufacturerID);
                            myProduct.SupplierName = (sdr["SupplierName"].ToString()!=""? sdr["SupplierName"].ToString():myProduct.SupplierName);
                            myProduct.ManufacturerName = (sdr["ManufacturerName"].ToString()!=""? sdr["ManufacturerName"].ToString():myProduct.ManufacturerName);
                            myProduct.BaseSellingPrice = sdr["BaseSellingPrice"].ToString() != "" ? decimal.Parse(sdr["BaseSellingPrice"].ToString()) : myProduct.BaseSellingPrice;
                            myProduct.CostPrice = sdr["CostPrice"].ToString() != "" ? decimal.Parse(sdr["CostPrice"].ToString()) : myProduct.CostPrice;
                            myProduct.ShortDescription = (sdr["ShortDescription"].ToString()!=""? sdr["ShortDescription"].ToString():myProduct.ShortDescription);
                            myProduct.LongDescription = (sdr["LongDescription"].ToString()!=""? sdr["LongDescription"].ToString():myProduct.LongDescription);
                            myProduct.ProductType = (sdr["ProductType"].ToString() != "" ? char.Parse(sdr["ProductType"].ToString()) : myProduct.ProductType);
                            myProduct.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : myProduct.StockAvailable);
                            myProduct.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? Int16.Parse(sdr["AttributeSetID"].ToString()) : myProduct.AttributeSetID;
                            myProduct.FreeDelivery = (sdr["FreeDeliveryYN"].ToString() != "" ? bool.Parse(sdr["FreeDeliveryYN"].ToString()) :myProduct.FreeDelivery );
                            myProduct.HeaderTags = (sdr["HeaderTag"].ToString()!=""? sdr["HeaderTag"].ToString():myProduct.HeaderTags);
                            myProduct.StickerURL = (sdr["StickerURL"].ToString()!=""?sdr["StickerURL"].ToString():myProduct.StickerURL);
                            myProduct.StickerID = (sdr["StickerID"].ToString() != "" ? Guid.Parse(sdr["StickerID"].ToString()) : myProduct.StickerID); //sdr["StickerID"].ToString();

                            myProduct.ProductHeaderImages = GetProductImages(myProduct.ID);

                            //myProduct.logDetails.CreatedBy = sdr["CreatedBy"].ToString();
                            //myProduct.logDetails.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["CreatedDate"].ToString())) : myProduct.logDetails.CreatedDate);
                            //myProduct.logDetails.UpdatedBy = sdr["UpdatedBy"].ToString();
                            //myProduct.logDetails.UpdatedDate = (sdr["UpdatedDate"].ToString() != "" ? (Convert.ToDateTime(sdr["UpdatedDate"].ToString())) : myProduct.logDetails.UpdatedDate);

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
           // AttributesRepository myAttributesRepository = new AttributesRepository(_databaseFactory);
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
                            myProductDetail.ID = sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myProductDetail.ID;
                            myProductDetail.AttributeSetID = sdr["AttributeSetID"].ToString() != "" ? Int16.Parse(sdr["AttributeSetID"].ToString()) : myProductDetail.AttributeSetID;
                            myProductDetail.Qty = sdr["Qty"].ToString() != "" ? Int16.Parse(sdr["Qty"].ToString()) : myProductDetail.Qty; //sdr["Qty"].ToString();
                            myProductDetail.OutOfStockAlertQty = sdr["OutOfStockAlertQty"].ToString() != "" ? Int16.Parse(sdr["OutOfStockAlertQty"].ToString()) : myProductDetail.OutOfStockAlertQty;
                            myProductDetail.PriceDifference = sdr["PriceDiffAmt"].ToString() != "" ? decimal.Parse(sdr["PriceDiffAmt"].ToString()) : myProductDetail.PriceDifference;
                            myProductDetail.Enabled = (sdr["EnableYN"].ToString() != "" ? bool.Parse(sdr["EnableYN"].ToString()) : myProductDetail.Enabled);
                            myProductDetail.DetailTags = (sdr["DetailTag"].ToString() != "" ? sdr["DetailTag"].ToString() : myProductDetail.DetailTags);
                            myProductDetail.StockAvailable = (sdr["StockAvailableYN"].ToString() != "" ? bool.Parse(sdr["StockAvailableYN"].ToString()) : myProductDetail.StockAvailable);
                            myProductDetail.DiscountAmount = sdr["DiscountAmout"].ToString() != "" ? decimal.Parse(sdr["DiscountAmout"].ToString()) : myProductDetail.DiscountAmount;
                            myProductDetail.DiscountStartDate = (sdr["DiscountStDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountStDate"].ToString())) : myProductDetail.DiscountStartDate);
                            myProductDetail.DiscountEndDate = (sdr["DiscountEnDate"].ToString() != "" ? (Convert.ToDateTime(sdr["DiscountEnDate"].ToString())) : myProductDetail.DiscountEndDate);

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
                                    if (sdr.GetOrdinal(att.Name) > 0)
                                        myAttribute.Value = sdr[att.Name].ToString();
                                }
                                catch (Exception)
                                {
                                }
                                myProductDetail.ProductAttributes.Add(myAttribute);
                            }
                            myProductDetails.Add(myProductDetail);
                            myProductDetail.ProductDetailImages = GetProductImages(myProductDetail.ProductID, myProductDetail.ID);

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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _productObj.ID);
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
                                        _productObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _productObj.ID);
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
                                operationsStatusObj.StatusMessage = "Not Successfull!";
                                return operationsStatusObj;
                            case "1":
                                //Delete Successfull
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = "Delete Successfull!";
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

    }
}