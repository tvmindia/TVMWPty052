using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class DynamicUIBusiness : IDynamicUIBusiness
    {
        private IDynamicUIRespository _dynamicUIRespository;
        /// <summary>
        /// Constructor Injection:-Getting IDynamicUIBusiness implementing object
        /// </summary>
        /// <param name="dynamicUIBusiness"></param>
        public DynamicUIBusiness(IDynamicUIRespository dynamicUIRespository)
        {
            _dynamicUIRespository = dynamicUIRespository;
        }

        public List<Menu> GetAllMenues()
        {
            try
            {
                return _dynamicUIRespository.GetAllMenues();
            }
            catch (Exception)
            {
                throw;
            }
            

        }




    }
}