using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface ICustomerBusiness
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int CustomerID, OperationsStatus Status);
        OperationsStatus CustomerEnableORDisable(Customer customer);
        OperationsStatus InsertUpdateCustomerAddress(Customer customer);
        List<CustomerAddress> GetAllCustomerAddresses(int CustomerID);
        Customer GetCustomerVerification(string Email);
        CustomerAddress GetAddressByAddress(int AddressID);
        OperationsStatus DeleteAddress(CustomerAddress customerAddress);
        OperationsStatus InsertCustomer(Customer customer);
        OperationsStatus UpdateCustomer(Customer customer);
    }
}
