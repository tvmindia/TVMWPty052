using AutoMapper;
using Newtonsoft.Json;
using PartyEC.BusinessServices.Contracts;
using PartyEC.DataAccessObject.DTO;
using PartyEC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PartyEC.UI.API
{
    public class OrderController : ApiController
    {
        #region Constructor_Injection

        IOrderBusiness _OrderBusiness;
        ICommonBusiness _commonBusiness;
        IBookingsBusiness _bookingBusiness;
        IQuotationsBusiness _quotationsBusiness;

        public OrderController(IOrderBusiness orderBusiness, ICommonBusiness commonBusiness, IBookingsBusiness bookingBusiness, IQuotationsBusiness quotationsBusiness)
        {
            _OrderBusiness = orderBusiness;
            _commonBusiness = commonBusiness;
            _bookingBusiness = bookingBusiness;
            _quotationsBusiness = quotationsBusiness;
        }
        #endregion Constructor_Injection

        [HttpPost]
        public object InsertBookings (Bookings BookingsObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                BookingsObj.logDetails = new LogDetails();
                BookingsObj.logDetails.CreatedBy = "AppUser";
                BookingsObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_bookingBusiness.InsertBookings(BookingsObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public object InsertQuotations(Quotations QuotationsObj)
        {
            OperationsStatusViewModel OperationsStatusViewModelObj = null;
            try
            {
                QuotationsObj.logDetails = new LogDetails();
                QuotationsObj.logDetails.CreatedBy = "AppUser";
                QuotationsObj.logDetails.CreatedDate = _commonBusiness.GetCurrentDateTime();

                OperationsStatusViewModelObj = Mapper.Map<OperationsStatus, OperationsStatusViewModel>(_quotationsBusiness.InsertQuotations(QuotationsObj));
                return JsonConvert.SerializeObject(new { Result = true, Records = OperationsStatusViewModelObj });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Result = false, Message = ex.Message });
            }
        }


    }
}
