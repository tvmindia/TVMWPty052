using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
   public interface ICategoriesBusiness
    {
        List<Categories> GetAllCategory();
        Categories GetCategory(int CategoryID, OperationsStatus Status);
        OperationsStatus InsertCategory(Categories CategoryObj);
        OperationsStatus UpdateCategory(Categories CategoryObj);
        OperationsStatus DeleteCategory(int CategoryID, OperationsStatus Status);
    }
}
