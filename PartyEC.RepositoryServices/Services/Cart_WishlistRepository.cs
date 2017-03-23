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
    public class Cart_WishlistRepository: ICart_WishlistRepository
    {
        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public Cart_WishlistRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public List<Cart_Wishlist> GetAllCustomerCartWishlistSummary()
        {
            List<Cart_Wishlist> Requestslist = null;
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
                        cmd.CommandText = "[GetAllCustomerCartWishlistSummary]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Requestslist = new List<Cart_Wishlist>();
                                while (sdr.Read())
                                {
                                    Cart_Wishlist _cartwishlistObj = new Cart_Wishlist();
                                    {
                                        _cartwishlistObj.ID = (sdr["ID"].ToString() != "" ? Int16.Parse(sdr["ID"].ToString()) : _cartwishlistObj.ID);
                                        _cartwishlistObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : _cartwishlistObj.Name);
                                        _cartwishlistObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : _cartwishlistObj.Email);
                                        _cartwishlistObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : _cartwishlistObj.Mobile);
                                        _cartwishlistObj.CartCount = (sdr["CartCount"].ToString() != "" ? Int16.Parse(sdr["CartCount"].ToString()) : _cartwishlistObj.CartCount);
                                        _cartwishlistObj.WishCount = (sdr["WishCount"].ToString() != "" ? Int16.Parse(sdr["WishCount"].ToString()) : _cartwishlistObj.WishCount);

                                    }
                                    Requestslist.Add(_cartwishlistObj);
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
        #endregion DataBaseFactory

    }
}