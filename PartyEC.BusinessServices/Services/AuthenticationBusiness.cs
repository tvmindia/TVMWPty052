using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;

namespace PartyEC.BusinessServices.Services
{
    public class AuthenticationBusiness: IAuthenticationBusiness
    {
        private IAuthenticationRepository _authenticationRepository;
        public AuthenticationBusiness(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roleList = null;
            try
            {
                roleList = _authenticationRepository.GetAllRoles();
            }
            catch(Exception ex)
            {

            }
            return roleList;
        }

        public List<User> GetAllUsers()
        {
            List<User> userList = null;
            try
            {
                userList = _authenticationRepository.GetAllUsers();
            }
            catch (Exception ex)
            {

            }
            return userList;
        }

    }
}