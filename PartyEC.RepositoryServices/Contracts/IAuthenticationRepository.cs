using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IAuthenticationRepository
    {
        List<Role> GetAllRoles();
        List<User> GetAllUsers();
        List<User> GetUserDetailByUser(int UserID);
        OperationsStatus InsertUser(User user);
        OperationsStatus UpdateUser(User user);
        OperationsStatus InsertUpdateUser(User user);
        OperationsStatus DeleteUser(int UserID);
    }
}
