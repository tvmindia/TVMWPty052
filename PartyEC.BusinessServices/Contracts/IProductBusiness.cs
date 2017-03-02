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
        OperationsStatus InsertProduct(Product productObj);
        
    }
}
