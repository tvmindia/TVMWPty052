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
    public class AuthenticationRepository: IAuthenticationRepository
    {
        private IDatabaseFactory _databaseFactory;
        public AuthenticationRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }



        public List<Role> GetAllRoles()
        {
            List<Role> roleList = null;
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
                        cmd.CommandText = "[GetAllRoles]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                roleList = new List<Role>();
                                while (sdr.Read())
                                {
                                    Role _roleObj = new Role();
                                    {
                                        _roleObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _roleObj.ID);
                                        _roleObj.RoleName = (sdr["RoleName"].ToString() != "" ? sdr["RoleName"].ToString() : _roleObj.RoleName);

                                        _roleObj.logDetails = new LogDetails();

                                        _roleObj.logDetails.CreatedDate = ((sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString()) : _roleObj.logDetails.CreatedDate));
                                       

                                    }
                                    roleList.Add(_roleObj);
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

            return roleList;


        }

        public List<User> GetAllUsers()
        {
            List<User> userList = null;
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
                        cmd.CommandText = "[GetAllUsers]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                userList = new List<User>();
                                while (sdr.Read())
                                {
                                    User _userObj = new User();
                                    {
                                        _userObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : _userObj.ID);
                                        _userObj.UserName = (sdr["UserName"].ToString() != "" ? sdr["UserName"].ToString() : _userObj.UserName);
                                        _userObj.ProfileImageId= (sdr["ProfileImageID"].ToString() != "" ? int.Parse(sdr["ProfileImageID"].ToString()) : _userObj.ProfileImageId);
                                        _userObj.RoleObj = new Role()
                                        {
                                            ID = (sdr["RoleID"].ToString() != "" ? int.Parse(sdr["RoleID"].ToString()) : 0),
                                            RoleName= (sdr["RoleName"].ToString() != "" ? sdr["RoleName"].ToString() : null)
                                        };
                                        _userObj.logDetails = new LogDetails();
                                        //orderObj.OrderDate = sdr["OrderDate"].ToString() != "" ? DateTime.Parse(sdr["OrderDate"].ToString()).ToString("dd-MMM-yyyy") : orderObj.OrderDate;
                                       // _userObj.logDetails.CreatedDate = ((sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(DateTime.Parse(sdr["CreatedDate"].ToString()).ToString("dd-MMM-yyyy")) : _userObj.logDetails.CreatedDate));


                                    }
                                    userList.Add(_userObj);
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

            return userList;


        }

    }
}