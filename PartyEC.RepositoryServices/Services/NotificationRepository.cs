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
    public class NotificationRepository: INotificationRepository
    {
        private Const constObj = new Const();
        private IDatabaseFactory _databaseFactory;
        public NotificationRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        #region GetAllNotifications
        public List<Notification> GetAllNotifications()
        {
            List<Notification> Notificationlist = null;
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
                        cmd.CommandText = "[GetAllNotifications]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Notificationlist = new List<Notification>();
                                while (sdr.Read())
                                {
                                    Notification _notificationObj = new Notification();
                                    {
                                        _notificationObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _notificationObj.ID);
                                        _notificationObj.Type = (sdr["Type"].ToString() != "" ? sdr["Type"].ToString() : _notificationObj.Type);
                                        _notificationObj.Message = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : _notificationObj.Message);
                                        _notificationObj.Title = (sdr["Title"].ToString() != "" ? sdr["Title"].ToString() : _notificationObj.Title);
                                        _notificationObj.Status = (sdr["Status"].ToString() != "" ?Int16.Parse(sdr["Status"].ToString()) : _notificationObj.Status);
                                        _notificationObj.logDetailsObj = new LogDetails();
                                        _notificationObj.logDetailsObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _notificationObj.logDetailsObj.CreatedDate);
                                        _notificationObj.customer = new Customer();
                                        {
                                            _notificationObj.customer.ID= (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _notificationObj.customer.ID);
                                            _notificationObj.customer.Name= (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _notificationObj.customer.Name);
                                        }
                                    }
                                    Notificationlist.Add(_notificationObj);
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
            return Notificationlist;
        }
        #endregion GetAllNotifications


        #region  GetNotification
        public Notification GetNotification(int ID)
        {
            Notification _notification = null;
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
                        cmd.Parameters.Add("@NotID", SqlDbType.Int).Value = ID;
                        cmd.CommandText = "[GetNotificationDetailsByNotification]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                if (sdr.Read())
                                {
                                    _notification = new Notification();
                                    _notification.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _notification.ID);
                                    _notification.Type = (sdr["Type"].ToString() != "" ? sdr["Type"].ToString() : _notification.Type);
                                    _notification.Title = (sdr["Title"].ToString() != "" ? sdr["Title"].ToString() : _notification.Title);
                                    _notification.Message = (sdr["Message"].ToString() != "" ? sdr["Message"].ToString() : _notification.Message);
                                    _notification.Status = (sdr["Status"].ToString() != "" ? Int16.Parse(sdr["Status"].ToString()) : _notification.Status);
                                    _notification.CustomerID= (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _notification.CustomerID);
                                    _notification.customer = new Customer();
                                    _notification.customer.ID= (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _notification.CustomerID);
                                    _notification.customer.Name = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _notification.customer.Name);
                                    _notification.logDetailsObj = new LogDetails();
                                    _notification.logDetailsObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _notification.logDetailsObj.CreatedDate);
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
            return _notification;
        }
        #endregion GetNotification

        #region NotificationPush
        public OperationsStatus NotificationPush(Notification notification)
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
                        cmd.CommandText = "[InsertNotification]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = notification.customer.ID;
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar,10).Value = notification.Type;
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar,250).Value = notification.Title;
                        cmd.Parameters.Add("@Message", SqlDbType.NVarChar, -1).Value = notification.Message;
                        cmd.Parameters.Add("@Status", SqlDbType.SmallInt).Value = notification.Status;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = notification.logDetailsObj.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = notification.logDetailsObj.CreatedDate;
                        statusCode = cmd.Parameters.Add("@StatusOut", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                // not Successfull                                
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                break;
                            case "1":
                                //Insert Successfull
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
                operationsStatusObj.StatusMessage = ex.Message;
                throw ex;
            }

            return operationsStatusObj;
        }
        #endregion NotificationPush
    }
}