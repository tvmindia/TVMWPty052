using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Services
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private ICustomerRepository _customerRepository;
        private IMailBusiness _mailBusiness;
        private IMasterRepository _masterRepository;

        public CustomerBusiness(ICustomerRepository customerRepository,IMailBusiness mailBusiness, IMasterRepository masterRepository)
        {
            _customerRepository = customerRepository;
            _mailBusiness = mailBusiness;
            _masterRepository = masterRepository;
        }


        #region Methods
        public List<Customer> GetAllCustomers()
        {
            List<Customer> CustomerLists = null;
            try
            {
                CustomerLists = _customerRepository.GetAllCustomers();

            }
            catch (Exception)
            {

            }
            return CustomerLists;
        }

        public Customer GetCustomer(int CustomerID, OperationsStatus Status)
        {
            Customer CustomerObj = null;
            try
            {
                CustomerObj = _customerRepository.GetCustomer(CustomerID, Status);

            }
            catch (Exception)
            {

            }
            return CustomerObj;
        }

        public OperationsStatus CustomerEnableORDisable(Customer customer)
        {
            OperationsStatus operationStatus = null;
            try
            {
                operationStatus = _customerRepository.CustomerEnableORDisable(customer);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return operationStatus;
        }

        public OperationsStatus InsertUpdateCustomerAddress(Customer customer)
        {
            OperationsStatus operationStatus = null;
            try
            {
                operationStatus = _customerRepository.InsertUpdateCustomerAddress(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationStatus;
        }

        public List<CustomerAddress> GetAllCustomerAddresses(int CustomerID)
        {
            List<CustomerAddress> AddressLists = null;
            try
            {
                AddressLists = _customerRepository.GetAllCustomerAddresses(CustomerID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return AddressLists;
        }

        public CustomerAddress GetAddressByAddress(int AddressID)
        {
            CustomerAddress Address = null;
            try
            {
                Address = _customerRepository.GetAddressByAddress(AddressID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Address;
        }

        public OperationsStatus DeleteAddress(CustomerAddress customerAddress)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj= _customerRepository.DeleteAddress(customerAddress);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;
        }

        public OperationsStatus InsertCustomer(Customer customer)
        {
            OperationsStatus operationStatus = null;
            try
            {
                operationStatus = _customerRepository.InsertCustomer(customer);
                if (operationStatus.StatusCode == 1 && customer.customerAddress!=null)
                {
                    customer.ID = int.Parse(operationStatus.ReturnValues.ToString());
                    customer.customerAddress.ShipDefaultYN = true;
                    customer.customerAddress.BillDefaultYN = true;
                    _customerRepository.InsertUpdateCustomerAddress(customer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationStatus;
        }

        public OperationsStatus UpdateCustomer(Customer customer)
        {
            OperationsStatus operationStatus = null;
            try
            {
                operationStatus = _customerRepository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationStatus;
        }

        public Customer GetCustomerVerification(string Email)
        {
            Customer CustomerStatus = null;
            try
            {
                CustomerStatus = _customerRepository.GetCustomerVerification(Email);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CustomerStatus;
        }

        public OperationsStatus SetDefaultAddress(int CustomerID, int AddressID)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _customerRepository.SetDefaultAddress(CustomerID,AddressID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationsStatusObj;

        }

        public async Task<bool> SendCustomerOTP(int OTP,string Email)
        {
                
            bool sendsuccess = false;
            try
            { 
                    Mail _mail = new Mail();
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/SendOtp.html")))
                    {
                        _mail.Body = reader.ReadToEnd();
                    }
                    _mail.Body = _mail.Body.Replace("{Otp}", OTP.ToString()); 
                    _mail.IsBodyHtml = true;
                    _mail.Subject = "OTP";
                    _mail.To = Email;
                    sendsuccess = await _mailBusiness.MailSendAsync(_mail);
                    //quotationsObj.EventsLogViewObj.CustomerNotifiedYN = Mailstatus;
                
            }
            catch (Exception ex)
            {
                throw ex;
                //return sendsuccess;
            }
            return sendsuccess;
        }

        public async Task<bool> SendContactUsEmail(ContactUs MailObj)
        {
            bool sendsuccess = false;
            try
            {
                Mail _mail = new Mail();
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/ContactUs.html")))
                {
                    _mail.Body = reader.ReadToEnd();
                }
                _mail.Body = _mail.Body.Replace("{Name}", MailObj.Name);
                _mail.Body = _mail.Body.Replace("{Email}", MailObj.Email);
                _mail.Body = _mail.Body.Replace("{Phone}", MailObj.Phone);
                _mail.Body = _mail.Body.Replace("{Comments}", MailObj.Comments);
                _mail.IsBodyHtml = true;
                _mail.Subject = "Contact US Requests";
                string EmailToAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
                _mail.To = EmailToAddress;
                sendsuccess = await _mailBusiness.MailSendAsync(_mail);
                sendsuccess = await SendCustomerContactUsEmailConfirmation(MailObj);
                //quotationsObj.EventsLogViewObj.CustomerNotifiedYN = Mailstatus;
            }
            catch (Exception ex)
            {
                throw ex;
                //return sendsuccess;
            }

            return sendsuccess;
        }

        public async Task<bool> SendCustomerContactUsEmailConfirmation(ContactUs MailObj)
        {
            bool sendsuccess = false;
            try
            {
                Mail _mail = new Mail();
                using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/CustomerContactUs.html")))
                {
                    _mail.Body = reader.ReadToEnd();
                }
                _mail.Body = _mail.Body.Replace("{Name}", MailObj.Name);
                _mail.Body = _mail.Body.Replace("{Email}", MailObj.Email);
                _mail.Body = _mail.Body.Replace("{Phone}", MailObj.Phone);
                _mail.Body = _mail.Body.Replace("{Comments}", MailObj.Comments);
                _mail.IsBodyHtml = true;
                _mail.Subject = "Contact US Requests";
                string EmailToAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
                _mail.To = MailObj.Email;
                sendsuccess = await _mailBusiness.MailSendAsync(_mail);

            }
            catch (Exception ex)
            {
                throw ex;
                //return sendsuccess;
            }
            return sendsuccess;
        }

        public OperationsStatus InsertCustomerImage(Customer customerObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                if (customerObj.ImageUrl != "" && customerObj.ImageUrl != null)
                {
                    OtherImages otherImgObj = new OtherImages();
                    otherImgObj.URL = customerObj.ImageUrl;
                    otherImgObj.ID = customerObj.ProfileImageID.Value;
                    otherImgObj.ImageType = ImageTypesPreffered.ProfileImage;
                    otherImgObj.LogDetails = customerObj.logDetailsObj;
                    operationsStatusObj = _masterRepository.InsertImage(otherImgObj);
                    if (operationsStatusObj.StatusCode == 1) {//Image id insertion success
                        operationsStatusObj = _customerRepository.UpdateCustomerImage(customerObj);//Updates customer image id and deletes old image from other images
                        if (operationsStatusObj.ReturnValues != null && operationsStatusObj.ReturnValues.ToString()!="")
                        {//Deletes old image file
                            File.Delete(HttpContext.Current.Server.MapPath(operationsStatusObj.ReturnValues.ToString()));
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