using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyEC.BusinessServices.Services
{
    public class ErrorLogBusiness : IErrorLogBusiness
    {
        #region ConstructorInjection

        private IErrorLogRepository _errorLogRepository;
        public ErrorLogBusiness(IErrorLogRepository errorLogRepository)
        {
            _errorLogRepository = errorLogRepository;
        }
        #endregion ConstructorInjection

        public OperationsStatus InsertErrorLog(ErrorLog ErrorObj)
        {
            OperationsStatus operationsStatusObj = null;
            try
            {
                operationsStatusObj = _errorLogRepository.InsertErrorLog(ErrorObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return operationsStatusObj;
        }
    }
}