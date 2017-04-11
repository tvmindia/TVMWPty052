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

    }
}