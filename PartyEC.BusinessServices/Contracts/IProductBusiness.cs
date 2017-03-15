using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IProductBusiness
    {
        List<Product> GetAllProducts(Product productObj);
        List<Product> GetAllProductswithCategory(Product productObj);
        List<Product> GetAssignedPro(string CategoryID);
        List<Product> GetUnAssignedPro(string CategoryID);
        OperationsStatus InsertProduct(Product productObj);
        Product GetProduct(int ProductID, OperationsStatus Status);
    }
}
