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
        private IDynamicUIRepository _dynamicUIRepository;
        /// <summary>
        /// Constructor Injection:-Getting IDynamicUIBusiness implementing object
        /// </summary>
        /// <param name="dynamicUIBusiness"></param>
        public DynamicUIBusiness(IDynamicUIRepository dynamicUIRespository)
        {
            _dynamicUIRepository = dynamicUIRespository;
        }

        public List<Menu> GetAllMenues()
        {
            try
            {
                return _dynamicUIRepository.GetAllMenues();
            }
            catch (Exception)
            {
                throw;
            }
            

        }




    }
}