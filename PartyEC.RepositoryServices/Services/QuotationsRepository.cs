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
    public class QuotationsRepository: IQuotationsRepository
    {

        Const constObj = new Const();

        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public QuotationsRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        public List<Quotations> GetAllQuotations()
        {
            List<Quotations> QuotationsList = null;

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
                        cmd.CommandText = "[GetAllQuotations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                QuotationsList = new List<Quotations>();
                                while (sdr.Read())
                                {
                                    Quotations _QuotationsObj = new Quotations();
                                    {
                                        _QuotationsObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _QuotationsObj.ID);
                                        _QuotationsObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _QuotationsObj.ProductID);
                                        _QuotationsObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _QuotationsObj.CustomerID);
                                        _QuotationsObj.QuotationNo = (sdr["QuotationNo"].ToString() != "" ? sdr["QuotationNo"].ToString() : _QuotationsObj.QuotationNo);
                                        _QuotationsObj.RequiredDate = (sdr["RequiredDate"].ToString() != "" ? DateTime.Parse(sdr["RequiredDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _QuotationsObj.RequiredDate);
                                        _QuotationsObj.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _QuotationsObj.QuotationDate);
                                        _QuotationsObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _QuotationsObj.Status);
                                        _QuotationsObj.customerObj = new Customer();
                                        _QuotationsObj.customerObj.Name = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _QuotationsObj.customerObj.Name);                                        
                                        _QuotationsObj.customerObj.Mobile = (sdr["CustomerMobile"].ToString() != "" ? sdr["CustomerMobile"].ToString() : _QuotationsObj.customerObj.Mobile);

                                    }
                                    QuotationsList.Add(_QuotationsObj);
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
            return QuotationsList;
        }

        public List<Quotations> GetCustomerQuotations(int CustomerID, bool IsHistory)
        {
            List<Quotations> QuotationsList = null;

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
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                        cmd.Parameters.Add("@IsHistory", SqlDbType.Int).Value = IsHistory;
                        cmd.CommandText = "[GetCustomerQuotations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                QuotationsList = new List<Quotations>();
                                while (sdr.Read())
                                {
                                    Quotations _QuotationsObj = new Quotations();
                                    {
                                        _QuotationsObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _QuotationsObj.ID);
                                        _QuotationsObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _QuotationsObj.ProductID);
                                        _QuotationsObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _QuotationsObj.CustomerID);
                                        _QuotationsObj.QuotationNo = (sdr["QuotationNo"].ToString() != "" ? sdr["QuotationNo"].ToString() : _QuotationsObj.QuotationNo);
                                        _QuotationsObj.RequiredDate = (sdr["RequiredDate"].ToString() != "" ? DateTime.Parse(sdr["RequiredDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _QuotationsObj.RequiredDate);
                                        _QuotationsObj.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _QuotationsObj.QuotationDate);
                                        _QuotationsObj.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : _QuotationsObj.Status);                                      
                                        _QuotationsObj.SourceIP = (sdr["SourceIP"].ToString() != "" ? sdr["SourceIP"].ToString() : _QuotationsObj.SourceIP);
                                        _QuotationsObj.StatusText = (sdr["StatusText"].ToString() != "" ? sdr["StatusText"].ToString() : _QuotationsObj.StatusText);
                                        _QuotationsObj.ProductName = (sdr["ProductName"].ToString() != "" ? sdr["ProductName"].ToString() : _QuotationsObj.ProductName); 
                                        _QuotationsObj.Qty = (sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : _QuotationsObj.Qty);
                                        _QuotationsObj.Price = (sdr["Price"].ToString() != "" ? decimal.Parse(sdr["Price"].ToString()) : _QuotationsObj.Price);
                                        _QuotationsObj.AdditionalCharges = (sdr["AdditionalCharges"].ToString() != "" ? decimal.Parse(sdr["AdditionalCharges"].ToString()) : _QuotationsObj.AdditionalCharges);
                                        _QuotationsObj.DiscountAmt = (sdr["DiscountAmt"].ToString() != "" ? decimal.Parse(sdr["DiscountAmt"].ToString()) : _QuotationsObj.DiscountAmt);
                                        _QuotationsObj.TaxAmt = (sdr["TaxAmt"].ToString() != "" ? decimal.Parse(sdr["TaxAmt"].ToString()) : _QuotationsObj.TaxAmt);
                                        _QuotationsObj.SubTotal = (sdr["SubTotal"].ToString() != "" ? decimal.Parse(sdr["SubTotal"].ToString()) : _QuotationsObj.SubTotal);
                                        _QuotationsObj.Total = (sdr["Total"].ToString() != "" ? decimal.Parse(sdr["Total"].ToString()) : _QuotationsObj.Total);
                                        _QuotationsObj.GrandTotal = (sdr["GrandTotal"].ToString() != "" ? decimal.Parse(sdr["GrandTotal"].ToString()) : _QuotationsObj.GrandTotal);
                                        _QuotationsObj.ProductSpecXML = (sdr["ProductSpecXML"].ToString() != "" ? sdr["ProductSpecXML"].ToString() : _QuotationsObj.ProductSpecXML);

                                        _QuotationsObj.customerObj = new Customer();
                                        _QuotationsObj.customerObj.Name = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _QuotationsObj.customerObj.Name);
                                        _QuotationsObj.customerObj.Mobile = (sdr["CustomerMobile"].ToString() != "" ? sdr["CustomerMobile"].ToString() : _QuotationsObj.customerObj.Mobile);
                                        _QuotationsObj.customerObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : _QuotationsObj.customerObj.Email);
                                        _QuotationsObj.ImageUrl = (sdr["ImageUrl"].ToString() != "" ? sdr["ImageUrl"].ToString() : _QuotationsObj.ImageUrl);

                                        _QuotationsObj.Message = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : _QuotationsObj.Message);
                                    }
                                    QuotationsList.Add(_QuotationsObj);
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
            return QuotationsList;
        }

        public Quotations GetQuotations(int QuotationsID)
        {
            Quotations myQuotations = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = QuotationsID;
                        cmd.CommandText = "[GetQuotations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    myQuotations = new Quotations();
                                    myQuotations.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : myQuotations.ID);                                  
                                    myQuotations.QuotationNo = (sdr["QuotationNo"].ToString() != "" ? sdr["QuotationNo"].ToString() : myQuotations.QuotationNo);
                                    myQuotations.QuotationDate = (sdr["QuotationDate"].ToString() != "" ? DateTime.Parse(sdr["QuotationDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : myQuotations.QuotationDate);
                                    myQuotations.RequiredDate = (sdr["RequiredDate"].ToString() != "" ? DateTime.Parse(sdr["RequiredDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : myQuotations.RequiredDate);
                                    myQuotations.SourceIP = (sdr["SourceIP"].ToString() != "" ? sdr["SourceIP"].ToString() : myQuotations.SourceIP);
                                    myQuotations.Status = (sdr["Status"].ToString() != "" ? sdr["Status"].ToString() : myQuotations.Status);
                                    myQuotations.StatusText = (sdr["StatusText"].ToString() != "" ? sdr["StatusText"].ToString() : myQuotations.StatusText);
                                    myQuotations.ProductName = (sdr["ProductName"].ToString() != "" ? sdr["ProductName"].ToString() : myQuotations.ProductName);
                                    myQuotations.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : myQuotations.ProductID);
                                    myQuotations.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : myQuotations.CustomerID); 
                                  
                                    myQuotations.Qty = (sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : myQuotations.Qty);
                                    myQuotations.Price = (sdr["Price"].ToString() != "" ? decimal.Parse(sdr["Price"].ToString()) : myQuotations.Price);
                                    myQuotations.AdditionalCharges = (sdr["AdditionalCharges"].ToString() != "" ? decimal.Parse(sdr["AdditionalCharges"].ToString()) : myQuotations.AdditionalCharges);
                                    myQuotations.DiscountAmt = (sdr["DiscountAmt"].ToString() != "" ? decimal.Parse(sdr["DiscountAmt"].ToString()) : myQuotations.DiscountAmt);
                                    myQuotations.TaxAmt = (sdr["TaxAmt"].ToString() != "" ? decimal.Parse(sdr["TaxAmt"].ToString()) : myQuotations.TaxAmt);
                                    myQuotations.SubTotal = (sdr["SubTotal"].ToString() != "" ? decimal.Parse(sdr["SubTotal"].ToString()) : myQuotations.SubTotal);
                                    myQuotations.Total = (sdr["Total"].ToString() != "" ? decimal.Parse(sdr["Total"].ToString()) : myQuotations.Total);
                                    myQuotations.GrandTotal = (sdr["GrandTotal"].ToString() != "" ? decimal.Parse(sdr["GrandTotal"].ToString()) : myQuotations.GrandTotal);                                    
                                    myQuotations.ProductSpecXML = (sdr["ProductSpecXML"].ToString() != "" ? sdr["ProductSpecXML"].ToString() : myQuotations.ProductSpecXML);
                                    
                                    myQuotations.customerObj = new Customer();
                                    myQuotations.customerObj.Name = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : myQuotations.customerObj.Name);
                                    myQuotations.customerObj.Mobile = (sdr["CustomerMobile"].ToString() != "" ? sdr["CustomerMobile"].ToString() : myQuotations.customerObj.Mobile);
                                    myQuotations.customerObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : myQuotations.customerObj.Email);
                                    myQuotations.ImageUrl = (sdr["ImageUrl"].ToString() != "" ? sdr["ImageUrl"].ToString() : myQuotations.ImageUrl);

                                    myQuotations.Message  = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : myQuotations.Message);                            
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
            return myQuotations;
        }

        public OperationsStatus InsertQuotations(Quotations quotationsObj)
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
                        cmd.CommandText = "[InsertQuotations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QuotationNo", SqlDbType.NVarChar, 20).Value = quotationsObj.QuotationNo;
                        cmd.Parameters.Add("@QuotationDate", SqlDbType.SmallDateTime).Value = quotationsObj.QuotationDate;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = quotationsObj.ProductID;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = quotationsObj.CustomerID;
                        cmd.Parameters.Add("@RequiredDate", SqlDbType.SmallDateTime).Value = quotationsObj.RequiredDate;
                        cmd.Parameters.Add("@SourceIP", SqlDbType.NVarChar, 50).Value = quotationsObj.SourceIP;
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = quotationsObj.Status;
                        cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = quotationsObj.Qty;                       
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, -1).Value = quotationsObj.Message;
                        cmd.Parameters.Add("@ProductSpecXML", SqlDbType.Xml).Value = quotationsObj.ProductSpecXML;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = quotationsObj.logDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = quotationsObj.logDetails.CreatedDate;
                        statusCode = cmd.Parameters.Add("@StatusOut", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.InsertSuccess;
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

        public OperationsStatus UpdateQuotations(Quotations quotationsObj)
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
                        cmd.CommandText = "[UpdateQuotations]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = quotationsObj.ID;
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = quotationsObj.Price;
                        cmd.Parameters.Add("@AdditionalCharges", SqlDbType.Decimal).Value = quotationsObj.AdditionalCharges;
                        cmd.Parameters.Add("@DiscountAmt", SqlDbType.Decimal).Value = quotationsObj.DiscountAmt;
                        cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal).Value = quotationsObj.TaxAmt;
                        cmd.Parameters.Add("@QuotationStatus", SqlDbType.VarChar, 50).Value = quotationsObj.Status;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                operationsStatusObj.ReturnValues = quotationsObj.ID;
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