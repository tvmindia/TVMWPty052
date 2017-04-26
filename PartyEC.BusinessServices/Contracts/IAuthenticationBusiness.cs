using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IAuthenticationBusiness
    {
        List<Role> GetAllRoles();
        List<User> GetAllUsers();
        List<User> GetUserDetailByUser(int UserID);
        OperationsStatus InsertUpdateUser(User user);
        OperationsStatus DeleteUser(int UserID, int LinkID);
    }
}
