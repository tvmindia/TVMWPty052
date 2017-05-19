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
        private Const constObj = new Const();
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
                                         _userObj.RoleList = (sdr["RoleList"].ToString() != "" ? sdr["RoleList"].ToString() : _userObj.RoleList);
                                        _userObj.UserName = (sdr["UserName"].ToString() != "" ? sdr["UserName"].ToString() : _userObj.UserName);
                                        _userObj.LoginName= (sdr["LoginName"].ToString() != "" ? sdr["LoginName"].ToString() : _userObj.LoginName);
                                        _userObj.Password = (sdr["Password"].ToString() != "" ? sdr["Password"].ToString() : _userObj.Password);
                                        _userObj.ProfileImageId= (sdr["ProfileImageID"].ToString() != "" ? Guid.Parse(sdr["ProfileImageID"].ToString()) : _userObj.ProfileImageId);
                                        if (!string.IsNullOrEmpty(_userObj.RoleList))
                                        {
                                            _userObj.Roles = _userObj.RoleList.Split(',').Select(t => t.Trim()).ToArray(); 
                                        }
                                        //_userObj.RoleObj = new Role()
                                        //{
                                        //    ID = (sdr["RoleID"].ToString() != "" ? int.Parse(sdr["RoleID"].ToString()) : 0),
                                        //    RoleName= (sdr["RoleName"].ToString() != "" ? sdr["RoleName"].ToString() : null)
                                        //};
                                       // _userObj.logDetails = new LogDetails();
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


        public List<User> GetUserDetailByUser(int UserID)
        {
            List<User>userList = null;
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
                        cmd.CommandText = "[GetUserDetailsByUser]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserID",SqlDbType.Int).Value= UserID;
                    
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
                                        _userObj.LoginName = (sdr["LoginName"].ToString() != "" ? sdr["LoginName"].ToString() : _userObj.LoginName);
                                        _userObj.UserName = (sdr["UserName"].ToString() != "" ? sdr["UserName"].ToString() : _userObj.UserName);
                                        _userObj.UserRoleLinkID= (sdr["UserRoleLinkID"].ToString() != "" ? int.Parse(sdr["UserRoleLinkID"].ToString()) : _userObj.ID);
                                        _userObj.ProfileImageId = (sdr["ProfileImageID"].ToString() != "" ? Guid.Parse(sdr["ProfileImageID"].ToString()) : _userObj.ProfileImageId);
                                        _userObj.RoleObj = new Role()
                                        {
                                            ID = (sdr["RoleID"].ToString() != "" ? int.Parse(sdr["RoleID"].ToString()) : 0),
                                            RoleName = (sdr["RoleName"].ToString() != "" ? sdr["RoleName"].ToString() : null)
                                        };
                                        _userObj.logDetails = new LogDetails();
                                        //orderObj.OrderDate = sdr["OrderDate"].ToString() != "" ? DateTime.Parse(sdr["OrderDate"].ToString()).ToString("dd-MMM-yyyy") : orderObj.OrderDate;
                                        _userObj.logDetails.CreatedDate = ((sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(DateTime.Parse(sdr["CreatedDate"].ToString()).ToString("dd-MMM-yyyy")) : _userObj.logDetails.CreatedDate));
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


        public OperationsStatus InsertUser(User user)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                SqlParameter statusCode = null;
                SqlParameter outparamID = null;
               
               // bool IsinsertOrUpdate = false;
                using (SqlConnection con = _databaseFactory.GetDBConnection())
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                  
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                        //if ((user.ID == 0)&&(user.UserRoleLinkID==0))
                        //{
                        //    cmd.CommandText = "[InsertUser]";
                        //    IsinsertOrUpdate = true;
                        //}
                        //else
                        //{
                        //    cmd.CommandText = "[UpdateUser]";
                        //}
                        cmd.CommandText = "[InsertUser]";
                        cmd.CommandType = CommandType.StoredProcedure;
                          //  cmd.Parameters.Add("@ID", SqlDbType.Int).Value = user.ID;
                           // cmd.Parameters.Add("@UserRoleLinkID", SqlDbType.Int).Value = user.UserRoleLinkID;
                            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar,250).Value = user.UserName;
                            cmd.Parameters.Add("@RoleList", SqlDbType.NVarChar,-1).Value = user.RoleList;
                            cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar,250).Value = user.LoginName;
                            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = user.Password;
                            cmd.Parameters.Add("@ProfileImageID", SqlDbType.UniqueIdentifier).Value = user.ProfileImageId;
                            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = user.logDetails.CreatedBy;
                            cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = user.logDetails.CreatedDate;
                        //  cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = user.logDetails.UpdatedBy;
                        //  cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = user.logDetails.UpdatedDate;

                        statusCode = cmd.Parameters.Add("@StatusOut", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        outparamID = cmd.Parameters.Add("@UserID", SqlDbType.Int);
                        outparamID.Direction = ParameterDirection.Output;
                      
                        cmd.ExecuteNonQuery();
                            operationsStatusObj = new OperationsStatus();
                            switch (Int16.Parse(statusCode.Value.ToString()))
                            {
                                case 0:
                                    // not Successfull                                
                                   operationsStatusObj.StatusMessage = constObj.InsertFailure;
                                   break;
                                case 1:
                                   operationsStatusObj.StatusMessage = constObj.InsertSuccess;
                                user.ID = int.Parse(outparamID.Value.ToString());
                              
                                operationsStatusObj.ReturnValues = new
                                {
                                    UserID = user.ID,
                                    
                                };
                                break;
                            case 2:
                                // not Successfull                                
                                operationsStatusObj.StatusMessage = constObj.Duplicate;

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


        public OperationsStatus UpdateUser(User user)
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
                        cmd.CommandText = "[UpdateUser]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = user.ID;
                        cmd.Parameters.Add("@RoleList", SqlDbType.NVarChar, -1).Value = user.RoleList;
                        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 250).Value = user.UserName;
                        cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = user.Password;
                        cmd.Parameters.Add("@ProfileImageID", SqlDbType.UniqueIdentifier).Value = user.ProfileImageId;
                        cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 250).Value = user.logDetails.UpdatedBy;
                        cmd.Parameters.Add("@UpdatedDate", SqlDbType.SmallDateTime).Value = user.logDetails.UpdatedDate;
                        statusCode = cmd.Parameters.Add("@StatusOut", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (Int16.Parse(statusCode.Value.ToString()))
                        {
                            case 0:
                                // not Successfull                                
                                operationsStatusObj.StatusMessage = constObj.UpdateFailure;
                                break;
                            case 1:
                                operationsStatusObj.StatusMessage = constObj.UpdateSuccess;
                                operationsStatusObj.ReturnValues = new
                                {
                                    UserID = user.ID,
                                    
                                };
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

        public OperationsStatus DeleteUser(int UserID)
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
                        cmd.CommandText = "[DeleteUser]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                        statusCode = cmd.Parameters.Add("@StatusOut", SqlDbType.SmallInt);
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

        public OperationsStatus InsertUpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}