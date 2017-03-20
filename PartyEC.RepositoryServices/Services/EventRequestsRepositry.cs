using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Data.SqlClient;
using System.Data;

namespace PartyEC.RepositoryServices.Services
{
    public class EventRequestsRepositry : IEventRequestsRepositry
    {
        Const ConstObj = new Const();
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public EventRequestsRepositry(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }
        #endregion DataBaseFactory

        #region Methods

        public List<EventRequests> GetAllEventRequests()
        {
            List<EventRequests> Requestslist = null;
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
                        cmd.CommandText = "[GetAllEventRequests]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Requestslist = new List<EventRequests>();
                                while (sdr.Read())
                                {
                                    EventRequests _eventRequestsObj = new EventRequests();
                                    {
                                        _eventRequestsObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _eventRequestsObj.ID);
                                        _eventRequestsObj.EventReqNo = (sdr["EventReqNo"].ToString() != "" ? sdr["EventReqNo"].ToString() : _eventRequestsObj.EventReqNo);
                                        _eventRequestsObj.EventType = (sdr["EventType"].ToString() != "" ? sdr["EventType"].ToString() : _eventRequestsObj.EventType);
                                        _eventRequestsObj.EventTitle = (sdr["EventTitle"].ToString() != "" ? sdr["EventTitle"].ToString() : _eventRequestsObj.EventTitle);
                                        _eventRequestsObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? Int16.Parse(sdr["CustomerID"].ToString()) : _eventRequestsObj.CustomerID);
                                        _eventRequestsObj.CustomerName = (sdr["CustomerName"].ToString() != "" ?sdr["CustomerName"].ToString() : _eventRequestsObj.CustomerName);
                                        _eventRequestsObj.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : _eventRequestsObj.ContactName);
                                        _eventRequestsObj.EventDateTime = (sdr["EventDateTime"].ToString() != "" ? DateTime.Parse(sdr["EventDateTime"].ToString()) : _eventRequestsObj.EventDateTime);
                                        _eventRequestsObj.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : _eventRequestsObj.Phone);
                                        _eventRequestsObj.EventStatus = (sdr["EventStatus"].ToString() != "" ? Int16.Parse(sdr["EventStatus"].ToString()) : _eventRequestsObj.EventStatus);
                                        _eventRequestsObj.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse( sdr["FollowUpDate"].ToString()) : _eventRequestsObj.FollowUpDate);
                                         
                                    }
                                    Requestslist.Add(_eventRequestsObj);
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
            return Requestslist;
        }

        public EventRequests GetEventRequest(int EventRequestsID, OperationsStatus Status)
        {
            EventRequests myEventRequests = null;
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
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = EventRequestsID;
                        cmd.CommandText = "[GetEventRequest]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    myEventRequests = new EventRequests();
                                    myEventRequests.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : myEventRequests.ID);
                                    myEventRequests.EventReqNo = (sdr["EventReqNo"].ToString() != "" ? sdr["EventReqNo"].ToString() : myEventRequests.EventReqNo);
                                    myEventRequests.EventType = (sdr["EventType"].ToString() != "" ? sdr["EventType"].ToString() : myEventRequests.EventType);
                                    myEventRequests.EventTitle = (sdr["EventTitle"].ToString() != "" ? sdr["EventTitle"].ToString() : myEventRequests.EventTitle);
                                   // myEventRequests.CustomerID = (sdr["CustomerID"].ToString() != "" ? Int16.Parse(sdr["CustomerID"].ToString()) : myEventRequests.CustomerID);
                                    myEventRequests.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : myEventRequests.ContactName);
                                    myEventRequests.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : myEventRequests.Email);
                                    myEventRequests.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : myEventRequests.Phone);
                                    myEventRequests.EventStatus = (sdr["EventStatus"].ToString() != "" ? Int16.Parse(sdr["EventStatus"].ToString()) : myEventRequests.EventStatus);
                                    myEventRequests.FollowUpDate = (sdr["FollowUpDate"].ToString() != "" ? DateTime.Parse(sdr["FollowUpDate"].ToString()) : myEventRequests.FollowUpDate);//

                                    myEventRequests.EventDateTime = (sdr["EventDateTime"].ToString() != "" ? DateTime.Parse(sdr["EventDateTime"].ToString()) : myEventRequests.EventDateTime);
                                    myEventRequests.NoOfPersons = (sdr["NoOfPersons"].ToString() != "" ? Int16.Parse(sdr["NoOfPersons"].ToString()) : myEventRequests.NoOfPersons);
                                    myEventRequests.Budget = (sdr["Budget"].ToString() != "" ? Decimal.Parse(sdr["Budget"].ToString()) : myEventRequests.Budget);
                                    myEventRequests.LookingFor = (sdr["LookingFor"].ToString() != "" ? sdr["LookingFor"].ToString() : myEventRequests.LookingFor);
                                    myEventRequests.RequirementSpec = (sdr["RequirementSpec"].ToString() != "" ? sdr["RequirementSpec"].ToString() : myEventRequests.RequirementSpec);
                                    //myEventRequests.CustomerID = (sdr["CustomerID"].ToString() != "" ? Int16.Parse(sdr["CustomerID"].ToString()) : myEventRequests.CustomerID);
                                    myEventRequests.ContactName = (sdr["ContactName"].ToString() != "" ? sdr["ContactName"].ToString() : myEventRequests.ContactName);
                                    myEventRequests.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : myEventRequests.Email);
                                    myEventRequests.Phone = (sdr["Phone"].ToString() != "" ? sdr["Phone"].ToString() : myEventRequests.Phone);
                                    myEventRequests.ContactType = (sdr["ContactType"].ToString() != "" ? sdr["ContactType"].ToString() : myEventRequests.ContactType);
                                    myEventRequests.Message = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : myEventRequests.Message);
                                    myEventRequests.AdminRemarks = (sdr["AdminRemarks"].ToString() != "" ? sdr["AdminRemarks"].ToString() : myEventRequests.AdminRemarks);
                                    myEventRequests.CurrencyCode = (sdr["CurrencyCode"].ToString() != "" ? sdr["CurrencyCode"].ToString() : myEventRequests.CurrencyCode);
                                    myEventRequests.CurrencyRate = (sdr["CurrencyRate"].ToString() != "" ? Decimal.Parse(sdr["CurrencyRate"].ToString()) : myEventRequests.CurrencyRate);
                                    myEventRequests.TotalAmt = (sdr["TotalAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalAmt"].ToString()) : myEventRequests.TotalAmt);
                                    myEventRequests.TotalTaxAmt = (sdr["TotalTaxAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalTaxAmt"].ToString()) : myEventRequests.TotalTaxAmt);
                                    myEventRequests.TotalDiscountAmt = (sdr["TotalDiscountAmt"].ToString() != "" ? Decimal.Parse(sdr["TotalDiscountAmt"].ToString()) : myEventRequests.TotalDiscountAmt);
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
            return myEventRequests;
        }

        public OperationsStatus UpdateEventRequests(EventRequests eventObj)
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
                        cmd.CommandText = "[UpdateEventRequests]";
                        cmd.CommandType = CommandType.StoredProcedure;
                      
                        cmd.Parameters.Add("@ID", SqlDbType.SmallInt).Value = eventObj.ID;
                        cmd.Parameters.Add("@Updateflag", SqlDbType.SmallInt).Value = eventObj.Updateflag;

                        cmd.Parameters.Add("@CurrencyCode", SqlDbType.NVarChar, 3).Value = eventObj.CurrencyCode;
                        cmd.Parameters.Add("@CurrencyRate", SqlDbType.Decimal).Value = eventObj.CurrencyRate;
                        cmd.Parameters.Add("@TotalAmt", SqlDbType.Decimal).Value = eventObj.TotalAmt;
                        cmd.Parameters.Add("@TotalTaxAmt", SqlDbType.Decimal).Value = eventObj.TotalTaxAmt;
                        cmd.Parameters.Add("@TotalDiscountAmt", SqlDbType.Decimal).Value = eventObj.TotalDiscountAmt;

                        cmd.Parameters.Add("@AdminRemarks", SqlDbType.NVarChar,-1).Value = eventObj.AdminRemarks;
                        cmd.Parameters.Add("@EventStatus", SqlDbType.SmallInt).Value = eventObj.EventStatus;
                        cmd.Parameters.Add("@FollowUpDate", SqlDbType.DateTime).Value = eventObj.FollowUpDate;

                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 10).Value = eventObj.logDetailsObj.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = eventObj.logDetailsObj.UpdatedDate;

                        outparameter = cmd.Parameters.Add("@Status", SqlDbType.SmallInt);
                        outparameter.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (outparameter.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(outparameter.Value.ToString());
                                operationsStatusObj.StatusMessage = ConstObj.UpdateSuccess;
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

        #endregion Methods

    }
}