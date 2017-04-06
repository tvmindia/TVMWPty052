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
        List<Categories> GetAllMainCategories();
        Categories GetCategory(int CategoryID);
        OperationsStatus InsertCategory(Categories CategoryObj);
        OperationsStatus UpdateCategory(Categories CategoryObj);
        OperationsStatus DeleteCategory(int CategoryID);
        OperationsStatus InsertImageCategory(Categories CategoryObj);
        OperationsStatus UpdatePositionNo(Categories CategoryObj);
        List<Categories> GetNavigationalCategoriesForApp(Categories categoryObj);
        List<Categories> GetFilterCategoriesForApp(Categories categoryObj);
    }
}
