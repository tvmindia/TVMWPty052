using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.CustomAttributes;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PartyEC.UI.API
{
    [CustomAuthenticationFilterForMobile]
    public class CommonController : ApiController
    {
        #region Constructor_Injection
        
        ICommonBusiness _commonBusiness;
        IErrorLogBusiness _errorBusiness;

        public CommonController(ICommonBusiness commonBusiness, IErrorLogBusiness errorBusiness)
        {
            _commonBusiness = commonBusiness;
            _errorBusiness = errorBusiness;
        }
        #endregion Constructor_Injection
        Const constants = new Const();

        [HttpPost]
        public object InsertErrorLog(ErrorLogAppViewModel ErrorLogAppViewModel)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                ErrorLog ErrorObj = new ErrorLog();
                ErrorObj.ErrorID = ErrorLogAppViewModel.REPORT_ID;
                ErrorObj.Description = "CRASH_CONFIGURATION\n\n" + JsonConvert.SerializeObject(ErrorLogAppViewModel.CRASH_CONFIGURATION)
                                    + "\n\nAVAILABLE_MEM_SIZE\n\n" + ErrorLogAppViewModel.AVAILABLE_MEM_SIZE
                                    + "\n\nREPORT_ID\n\n" + ErrorLogAppViewModel.REPORT_ID;
                ErrorObj.AppBuild = JsonConvert.SerializeObject(ErrorLogAppViewModel.BUILD);
                ErrorObj.AppLogCat = ErrorLogAppViewModel.LOGCAT;
                ErrorObj.Module = ErrorLogAppViewModel.PACKAGE_NAME;
                ErrorObj.ErrorSource = "App";
                ErrorObj.IsMobile = true;
                ErrorObj.Version = ErrorLogAppViewModel.ANDROID_VERSION + "/" + ErrorLogAppViewModel.APP_VERSION_CODE;
                ErrorObj.commonObj = new LogDetails();
                ErrorObj.commonObj.CreatedBy = _commonBusiness.GetUA().UserName;
                ErrorObj.commonObj.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_errorBusiness.InsertErrorLog(ErrorObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

    }
}