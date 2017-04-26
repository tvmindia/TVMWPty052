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

        public OperationsStatus DeleteUser(int UserID, int LinkID)
        {
            OperationsStatus operationsStatus = null;
            try
            {
                operationsStatus= _authenticationRepository.DeleteUser(UserID, LinkID);
            }
            catch(Exception ex)
            {

            }
            return operationsStatus;
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roleList = null;
            try
            {
                roleList = _authenticationRepository.GetAllRoles();
                roleList = roleList == null ? null : roleList.OrderBy(ro => ro.RoleName).ToList();
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

        public List<User> GetUserDetailByUser(int UserID)
        {
            List<User> userLsit = null;
            try
            {
                userLsit = _authenticationRepository.GetUserDetailByUser(UserID);
            }
            catch(Exception ex)
            {

            }
            return userLsit;
        }

        public OperationsStatus InsertUpdateUser(User user)
        {
            OperationsStatus operationsStatus = null;
            try
            {
                if((user.ID==0)&&(user.UserRoleLinkID==0))
                {
                    operationsStatus = _authenticationRepository.InsertUser(user);
                }
                else
                {
                    operationsStatus = _authenticationRepository.UpdateUser(user);
                }

            }
            catch (Exception ex)
            {

            }
            return operationsStatus;
        }
    }
}