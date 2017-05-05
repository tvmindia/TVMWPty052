using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Text;
using System.Security.Cryptography;

namespace PartyEC.BusinessServices.Services
{
    public class AuthenticationBusiness: IAuthenticationBusiness
    {
        //Encryption key
        string key = System.Web.Configuration.WebConfigurationManager.AppSettings["cryptography"];
        private IAuthenticationRepository _authenticationRepository;
        public AuthenticationBusiness(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public OperationsStatus DeleteUser(int UserID)
        {
            OperationsStatus operationsStatus = null;
            try
            {
                operationsStatus= _authenticationRepository.DeleteUser(UserID);
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
                userList = userList == null ? null : userList.
                Where(us => !us.Roles.Contains("SA"))
                .Select(c => { c.Password = null; return c; }).ToList();
                //userList.Select(c => { c.Password = null; return c; }).ToList();
            }
            catch (Exception ex)
            {

            }
            return userList;
        }

        public User CheckUserCredentials(User user)
        {
            User _user = null;
            List<User> userList = null;

            try
            {
                //  var found = (from c in c.Options.OfType<Option>()
                //               where c.StoredValue == yourValue
                //               select c.DisplayName).FirstOrDefault();
                userList = _authenticationRepository.GetAllUsers();
                userList = userList == null ? null : userList.Where(us => us.LoginName.ToLower() == user.LoginName.ToLower() && Decrypt(us.Password)==user.Password).ToList();

                _user = userList == null ? null : userList[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _user;

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
                //Encryption
                if ((!string.IsNullOrEmpty(user.LoginName))&&(!string.IsNullOrEmpty(user.Password)))
                {
                    user.Password = Encrypt(user.Password);
                    //case sensitive login logic-making it lower case
                    user.LoginName = user.LoginName.ToLower();
                switch (user.ID)
                   {
                    case 0:
                       
                        operationsStatus = _authenticationRepository.InsertUser(user);
                    break;
                    default:
                        
                        operationsStatus = _authenticationRepository.UpdateUser(user);
                    break;
                   }
                }

            }
            catch (Exception ex)
            {

            }
            return operationsStatus;
        }

        private string Encrypt(string plainText)
        {
            //AES 128bit Cross Platform (Java and C#) Encryption Compatibility
            
            string encryptedText = "";
            try
            {

                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                var keyBytes = new byte[16];
                var secretKeyBytes = Encoding.UTF8.GetBytes(key);
                Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
                encryptedText = Convert.ToBase64String(new RijndaelManaged
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 128,
                    BlockSize = 128,
                    Key = keyBytes,
                    IV = keyBytes
                }.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return encryptedText;
        }

        private string Decrypt(string encryptedText)
        {
            string plainText = "";
            try
            {
                var encryptedBytes = Convert.FromBase64String(encryptedText);
                var keyBytes = new byte[16];
                var secretKeyBytes = Encoding.UTF8.GetBytes(key);
                Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
                plainText = Encoding.UTF8.GetString(
                    new RijndaelManaged
                    {
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7,
                        KeySize = 128,
                        BlockSize = 128,
                        Key = keyBytes,
                        IV = keyBytes
                    }.CreateDecryptor().TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
            }
            catch (Exception ex)
            {
            }
            return plainText;
        }

    }
}