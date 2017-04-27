using PartyEC.BusinessServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;

namespace PartyEC.BusinessServices.Services
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private ICustomerRepository _customerRepository;

        public CustomerBusiness(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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

            }
            return operationsStatusObj;
        }

        public OperationsStatus InsertCustomer(Customer customer)
        {
            OperationsStatus operationStatus = null;
            try
            {
                operationStatus = _customerRepository.InsertCustomer(customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return operationStatus;
        }

        #endregion Methods
    }
}