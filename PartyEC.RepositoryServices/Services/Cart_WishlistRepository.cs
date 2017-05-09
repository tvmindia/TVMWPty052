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
        Const constObj = new Const();

        #region DataBaseFactory
        private IDatabaseFactory _databaseFactory;
        private IAttributesRepository _attributesRepository;
        /// <summary>
        /// Constructor Injection:-Getting IDatabaseFactory implemented object
        /// </summary>
        /// <param name="databaseFactory"></param>
        public Cart_WishlistRepository(IDatabaseFactory databaseFactory, IAttributesRepository attributesRepository)
        {
            _databaseFactory = databaseFactory;
            _attributesRepository = attributesRepository;
        }         
        #endregion DataBaseFactory

        public List<Customer> GetAllCustomerCartWishlistSummary()
        {
            List<Customer> Requestslist = null;
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
                                Requestslist = new List<Customer>();
                                while (sdr.Read())
                                {
                                    Customer CustomerObj = new Customer();
                                    {
                                        CustomerObj.ID = (sdr["ID"].ToString() != "" ? int.Parse(sdr["ID"].ToString()) : CustomerObj.ID);
                                        CustomerObj.Name = (sdr["Name"].ToString() != "" ? sdr["Name"].ToString() : CustomerObj.Name);
                                        CustomerObj.Email = (sdr["Email"].ToString() != "" ? sdr["Email"].ToString() : CustomerObj.Email);
                                        CustomerObj.Mobile = (sdr["Mobile"].ToString() != "" ? sdr["Mobile"].ToString() : CustomerObj.Mobile);
                                        CustomerObj.CartCount = (sdr["CartCount"].ToString() != "" ? Int16.Parse(sdr["CartCount"].ToString()) : CustomerObj.CartCount);
                                        CustomerObj.WishCount = (sdr["WishCount"].ToString() != "" ? Int16.Parse(sdr["WishCount"].ToString()) : CustomerObj.WishCount);

                                    }
                                    Requestslist.Add(CustomerObj);
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

        public List<ShoppingCart> GetCustomerShoppingCart(int customerID,int locationID)
        {
            List<ShoppingCart> Cartlist = null;
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
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
                        cmd.Parameters.Add("@locationID", SqlDbType.Int).Value = locationID;
                        cmd.CommandText = "[GetCustomerShoppingCart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Cartlist = new List<ShoppingCart>();
                                while (sdr.Read())
                                {
                                    ShoppingCart _ShoppingcartObj = new ShoppingCart();
                                    {
                                        _ShoppingcartObj.ID = (sdr["CartId"].ToString() != "" ? int.Parse(sdr["CartId"].ToString()) : _ShoppingcartObj.ID);
                                        _ShoppingcartObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _ShoppingcartObj.ProductID);
                                        _ShoppingcartObj.ProductName = (sdr["ProductName"].ToString() != "" ?  sdr["ProductName"].ToString() : _ShoppingcartObj.ProductName);
                                        _ShoppingcartObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _ShoppingcartObj.CustomerID);
                                        _ShoppingcartObj.CustomerName = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _ShoppingcartObj.CustomerName);
                                        _ShoppingcartObj.ProductSpecXML = (sdr["ProductSpecXML"].ToString() != "" ? sdr["ProductSpecXML"].ToString() : _ShoppingcartObj.ProductSpecXML);
                                        _ShoppingcartObj.Qty = (sdr["Qty"].ToString() != "" ? int.Parse(sdr["Qty"].ToString()) : _ShoppingcartObj.Qty);
                                        _ShoppingcartObj.CurrencyCode = (sdr["CurrencyCode"].ToString() != "" ? sdr["CurrencyCode"].ToString() : _ShoppingcartObj.CurrencyCode);
                                        _ShoppingcartObj.Price = (sdr["Price"].ToString() != "" ? Decimal.Parse(sdr["Price"].ToString()) : _ShoppingcartObj.Price);
                                        _ShoppingcartObj.ItemStatus = (sdr["ItemStatus"].ToString() != "" ? sdr["ItemStatus"].ToString() : _ShoppingcartObj.ItemStatus);

                                        _ShoppingcartObj.ImageURL = (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _ShoppingcartObj.ImageURL);
                                        _ShoppingcartObj.ShippingCharge = (sdr["ShippingCharge"].ToString() != "" ? Decimal.Parse(sdr["ShippingCharge"].ToString()) : _ShoppingcartObj.ShippingCharge); 
                                        _ShoppingcartObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _ShoppingcartObj.CreatedDate);    
                                    }
                                    Cartlist.Add(_ShoppingcartObj);
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
            return Cartlist;
        }
        
        public List<Wishlist> GetCustomerWishlist(int customerID,string CurrentDate)
        {
            List<Wishlist> Requestslist = null;
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
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
                        if (CurrentDate!="" && CurrentDate!=null) 
                        cmd.Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = DateTime.Parse(CurrentDate);
                        cmd.CommandText = "[GetCustomerWishlist]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if ((sdr != null) && (sdr.HasRows))
                            {
                                Requestslist = new List<Wishlist>();
                                while (sdr.Read())
                                {
                                    Wishlist _wishlistObj = new Wishlist();
                                    {
                                        _wishlistObj.ProductID = (sdr["ProductID"].ToString() != "" ? int.Parse(sdr["ProductID"].ToString()) : _wishlistObj.ProductID);
                                        _wishlistObj.ProductName = (sdr["ProductName"].ToString() != "" ? sdr["ProductName"].ToString() : _wishlistObj.ProductName);
                                        _wishlistObj.CustomerID = (sdr["CustomerID"].ToString() != "" ? int.Parse(sdr["CustomerID"].ToString()) : _wishlistObj.CustomerID);
                                        _wishlistObj.CustomerName = (sdr["CustomerName"].ToString() != "" ? sdr["CustomerName"].ToString() : _wishlistObj.CustomerName);
                                        _wishlistObj.ProductSpecXML = (sdr["ProductSpecXML"].ToString() != "" ? sdr["ProductSpecXML"].ToString() : _wishlistObj.ProductSpecXML);    
                                        _wishlistObj.DaysinWL = (sdr["DaysinWL"].ToString() != "" ? sdr["DaysinWL"].ToString() : _wishlistObj.DaysinWL);
                                        _wishlistObj.CreatedDate = (sdr["CreatedDate"].ToString() != "" ? DateTime.Parse(sdr["CreatedDate"].ToString().ToString()).ToString("dd-MMM-yyyy") : _wishlistObj.CreatedDate);
                                        _wishlistObj.ImageURL = (sdr["ImageURL"].ToString() != "" ? sdr["ImageURL"].ToString() : _wishlistObj.ImageURL);
                                        _wishlistObj.Price = (sdr["TotalPrice"].ToString() != "" ? Decimal.Parse(sdr["TotalPrice"].ToString()) : _wishlistObj.Price);
                                    }
                                    Requestslist.Add(_wishlistObj);
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

        public OperationsStatus AddProductToCart(ShoppingCart cartObj)
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
                        cmd.CommandText = "[InsertShoppingCart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = cartObj.ProductID;
                        cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = cartObj.CustomerID;
                        cmd.Parameters.Add("@ProductSpecXML", SqlDbType.Xml).Value =_attributesRepository.GetAttributeXML(cartObj.AttributeValues);
                        cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = cartObj.Qty;
                        cmd.Parameters.Add("@CurrencyCode", SqlDbType.NVarChar, 3).Value = cartObj.CurrencyCode;
                        cmd.Parameters.Add("@CurrencyRate", SqlDbType.Decimal).Value = cartObj.CurrencyRate;
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = cartObj.Price;
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 250).Value = cartObj.logDetails.CreatedBy;
                        cmd.Parameters.Add("@CreatedDate", SqlDbType.SmallDateTime).Value = cartObj.logDetails.CreatedDate;
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

        public OperationsStatus RemoveProductFromCart(ShoppingCart cartObj)
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
                        cmd.CommandText = "[DeleteProductFromCart]";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = cartObj.ID;
                        statusCode = cmd.Parameters.Add("@StatusOut", SqlDbType.SmallInt);
                        statusCode.Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        operationsStatusObj = new OperationsStatus();
                        switch (statusCode.Value.ToString())
                        {
                            case "0":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
                                operationsStatusObj.StatusMessage = constObj.DeleteFailure;
                                break;
                            case "1":
                                operationsStatusObj.StatusCode = Int16.Parse(statusCode.Value.ToString());
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
    }
}