using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PartyEC.UI.Models;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using AutoMapper;
using Newtonsoft.Json;
using System.Threading.Tasks;
using PartyEC.UI.CustomAttributes;
using System.Web;
using System.IO;

namespace PartyEC.UI.API
{
    [CustomAuthenticationFilterForMobile]
    public class CustomerController : ApiController
    {

        #region Constructor_Injection

        ICustomerBusiness _customerBusiness;
        ICommonBusiness _commonBusiness;
        IMasterBusiness _masterBusiness;
        IEventBusiness _eventBusiness;
        ICart_WishlistBusiness _cartwishlistBusiness;
        IOrderBusiness _orderBusiness;
        IQuotationsBusiness _quotationBusiness;
        IBookingsBusiness _bookingsBusiness;

        public CustomerController(ICustomerBusiness customerBusiness,IMasterBusiness masterBusiness,ICommonBusiness commonBusiness, IEventBusiness eventBusiness,ICart_WishlistBusiness cartwishlistBusiness, IOrderBusiness orderBusiness, IQuotationsBusiness quotationBusiness, IBookingsBusiness bookingsBusiness)
        {
            _customerBusiness = customerBusiness;
            _commonBusiness = commonBusiness;
            _eventBusiness = eventBusiness;
            _masterBusiness = masterBusiness;
            _cartwishlistBusiness = cartwishlistBusiness;
             _orderBusiness=orderBusiness;
            _quotationBusiness = quotationBusiness;
            _bookingsBusiness = bookingsBusiness;
        }
        #endregion Constructor_Injection
        Const constants = new Const();

        #region shoppingCart 

        [HttpPost]
        public object GetCustomerShoppingCart(ShoppingCart CartWishObj)
        {
            try
            {
                CartWishObj.logDetails = new LogDetails();
                CartWishObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();//passing Current Date
                List<ShoppingCartViewModel> CartList = Mapper.Map<List<ShoppingCart>, List<ShoppingCartViewModel>>(_cartwishlistBusiness.GetCustomerShoppingCart(CartWishObj));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion shoppingCart
        
        #region Wishlist
        [HttpPost]
        public object GetCustomerWishlist(Wishlist WishlistObj)
        {
            try
            { 
                List<WishlistViewModel> CartList = Mapper.Map<List<Wishlist>, List<WishlistViewModel>>(_cartwishlistBusiness.GetCustomerWishlist(WishlistObj.CustomerID, _commonBusiness.GetCurrentDateTime().ToString()));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion Wishlist

        #region Orders

        [HttpPost]
        public object GetCustomerOrders(Order OrderObj)
        {
            try
            {
                bool Ishistory = false;
                List<OrderAppViewModel> CartList = Mapper.Map<List<Order>, List<OrderAppViewModel>>(_orderBusiness.GetCustomerOrders(OrderObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCustomerOrderDetails(OrderDetail OrderObj)
        {
            try
            {
                string OrderId = OrderObj.OrderID.ToString();
                List<OrderAppViewModel> CartList = Mapper.Map<List<OrderDetail>, List<OrderAppViewModel>>(_orderBusiness.GetAllOrdersList(OrderId));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCustomerOrdersHistory(Order OrderObj)
        {
            try
            {
                bool Ishistory = true;
                List<OrderAppViewModel> CartList = Mapper.Map<List<Order>, List<OrderAppViewModel>>(_orderBusiness.GetCustomerOrders(OrderObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion Orders

        #region Quotations
        [HttpPost]
        public object GetCustomerQuotations(Quotations QuotationsObj)
        {
            try
            {
                bool Ishistory = false;
                List<QuotationsAppViewModel> CartList = Mapper.Map<List<Quotations>, List<QuotationsAppViewModel>>(_quotationBusiness.GetCustomerQuotations(QuotationsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


        [HttpPost]
        public object GetCustomerQuotationsHistory(Quotations QuotationsObj)
        {           
            try
            {
                bool Ishistory = true;
                List<QuotationsAppViewModel> CartList = Mapper.Map<List<Quotations>, List<QuotationsAppViewModel>>(_quotationBusiness.GetCustomerQuotations(QuotationsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }



        #endregion Quotations

        #region Bookings
        [HttpPost]
        public object GetCustomerBookings(Bookings bookingsObj)
        {
            try
            {
                bool Ishistory = false;
                List<BookingsAppViewModel> CartList = Mapper.Map<List<Bookings>, List<BookingsAppViewModel>>(_bookingsBusiness.GetCustomerBookings(bookingsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCustomerBookingsHistory(Bookings bookingsObj)
        {
            try
            {
                bool Ishistory = true;
                List<BookingsAppViewModel> CartList = Mapper.Map<List<Bookings>, List<BookingsAppViewModel>>(_bookingsBusiness.GetCustomerBookings(bookingsObj.CustomerID, Ishistory));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        #endregion Bookings

        #region Address
        [HttpPost]
        public object InsertUpdateCustomerAddress(Customer addressObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                addressObj.logDetailsObj = new LogDetails();
                addressObj.logDetailsObj.CreatedBy = _commonBusiness.GetUA().UserName;
                addressObj.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                addressObj.logDetailsObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                addressObj.logDetailsObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.InsertUpdateCustomerAddress(addressObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCustomerAddress(CustomerAddress addressObj)
        {
            try
            {
              
                List<CustomerAddressViewModel> CartList = Mapper.Map<List<CustomerAddress>, List<CustomerAddressViewModel>>(_customerBusiness.GetAllCustomerAddresses(addressObj.CustomerID));
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object DeleteCustomerAddress(CustomerAddress addressObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {            
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.DeleteAddress(addressObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object SetDefaultAddress(CustomerAddress addressObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.SetDefaultAddress(addressObj.CustomerID,addressObj.ID));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
        #endregion Address

        #region User
        [HttpPost]
        public object RegisterUser (Customer customerObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                customerObj.logDetailsObj = new LogDetails();
                customerObj.logDetailsObj.CreatedBy = _commonBusiness.GetUA().UserName;
                customerObj.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.InsertCustomer(customerObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object UpdateUser(Customer customerObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                customerObj.logDetailsObj = new LogDetails();
                customerObj.logDetailsObj.UpdatedBy = _commonBusiness.GetUA().UserName;
                customerObj.logDetailsObj.UpdatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.UpdateCustomer(customerObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
                
        [HttpPost]
        public string UploadProfileImage()
        {
            try
            {
                //Getting other details on object
                Customer customer = new Customer();

                HttpFileCollection MyFileCollection = HttpContext.Current.Request.Files;
                //Getting file dettails from http request
                if (MyFileCollection.Count > 0 && !string.IsNullOrEmpty(HttpContext.Current.Request.Form["CustomerID"]))
                {
                    string ImageID = Guid.NewGuid().ToString();
                    string imagePath = ImageID + "." + MyFileCollection[0].FileName;//.Split('.').Last();
                    string FilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\Content\CustomerImages\") + imagePath;
                    MyFileCollection[0].SaveAs(FilePath); //to save incoming image to server folder
               
                    customer.ID = int.Parse(HttpContext.Current.Request.Form["CustomerID"]);
                    customer.ImageUrl = "/Content/CustomerImages/" + imagePath;
                    customer.ProfileImageID = Guid.Parse(ImageID);

                    customer.logDetailsObj = new LogDetails();
                    customer.logDetailsObj.CreatedBy = _commonBusiness.GetUA().UserName;
                    customer.logDetailsObj.CreatedDate = _commonBusiness.GetCurrentDateTime();
                    customer.logDetailsObj.UpdatedBy = customer.logDetailsObj.CreatedBy;
                    customer.logDetailsObj.UpdatedDate = customer.logDetailsObj.CreatedDate;

                    OperationsStatusViewModel OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_customerBusiness.InsertCustomerImage(customer));
                }
                else
                {
                    throw new Exception(constants.UpdateFailure);
                }
                return JsonConvert.SerializeObject(new { Result = false, Message = constants.UpdateSuccess , FilePath = customer.ImageUrl });
            }
            catch (Exception ex)
            {
                //Return error message
                //File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath(@"~\Content\CustomerImages\file.txt"), ex.Message);
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });                
            }
        }
        #endregion User

        [HttpPost]
        public async Task<object> GetCustomerVerificationandOTP(CustomerViewModel customerObj)
        {
            try
            {
                bool flag = true;
               
                int OTP;

                CustomerViewModel CustomerList = Mapper.Map<Customer,CustomerViewModel>(_customerBusiness.GetCustomerVerification(customerObj.Email));
                if (CustomerList == null)
                {
                      flag = false;
                } 
                Random rnd = new Random();                  // Random number creation for OTP
                OTP= rnd.Next(2000, 9000);
                //sending otp to mail.
                await _customerBusiness.SendCustomerOTP(OTP, customerObj.Email);
                return JsonConvert.SerializeObject(new { Result = true, Records = new { Customer = CustomerList, IsUser = flag, CustomerOTP = OTP } });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetShippingLocations()
        {
            try
            {
                List<ShippingLocationViewModel> CartList = Mapper.Map<List<ShippingLocations>, List<ShippingLocationViewModel>>(_masterBusiness.GetAllShippingLocation());
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object GetCountries()
        {
            try
            {
                List<CountryViewModel> CartList = Mapper.Map<List<Country>, List<CountryViewModel>>(_masterBusiness.GetAllCountries());
                if (CartList.Count == 0) throw new Exception(constants.NoItems);
                return JsonConvert.SerializeObject(new { Result = true, Records = CartList });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<object> SendContactUsEmail( ContactUs MailObj)
        {
            bool  MailStatus = false;
            try
            {
                MailStatus = await _customerBusiness.SendContactUsEmail(MailObj);
                return JsonConvert.SerializeObject(new { Result = true, Records = MailStatus });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<object> SendCustomerContactUsEmailConfirmation(ContactUs MailObj)
        {
            bool MailStatus = false;
            try
            {
                MailStatus = await _customerBusiness.SendCustomerContactUsEmailConfirmation(MailObj);
                return JsonConvert.SerializeObject(new { Result = true, Records = MailStatus });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }
    }
}
