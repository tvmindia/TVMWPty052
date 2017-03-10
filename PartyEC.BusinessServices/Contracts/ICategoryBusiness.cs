using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
   public interface ICategoryBusiness
    {
        List<Category> GetAllCategory();
        Category GetCategory(int CategoryID, OperationsStatus Status);
        OperationsStatus InsertCategory(Category CategoryObj);
        OperationsStatus UpdateCategory(Category CategoryObj);
        OperationsStatus DeleteCategory(int CategoryID, OperationsStatus Status);
    }
}
