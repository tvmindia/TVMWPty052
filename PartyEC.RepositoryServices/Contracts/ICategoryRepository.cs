using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartyEC.DataAccessObject.DTO;


namespace PartyEC.RepositoryServices.Contracts
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategory();
        Category GetCategory(int CategoryID, OperationsStatus Status);
        OperationsStatus InsertCategory(Category CategoryObj);
        OperationsStatus UpdateCategory(Category CategoryObj);
        OperationsStatus DeleteCategory(int CategoryID, OperationsStatus Status);
    }
}
