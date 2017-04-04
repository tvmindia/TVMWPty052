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
        #endregion Methods
    }
}