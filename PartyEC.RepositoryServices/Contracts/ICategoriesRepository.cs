using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyEC.DataAccessObject.DTO;


namespace PartyEC.RepositoryServices.Contracts
{
    public interface ICategoriesRepository
    {
        List<Categories> GetAllCategory();
        Categories GetCategory(int CategoryID);
        OperationsStatus InsertCategory(Categories CategoryObj);
        OperationsStatus UpdateCategory(Categories CategoryObj);
        OperationsStatus DeleteCategory(int CategoryID);
        bool ExistOrNot(int CategoryID);
    }
}
