using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;
using System.Threading.Tasks;
using System.IO;

namespace PartyEC.BusinessServices.Services
{
    public class BookingsBusiness: IBookingsBusiness
    {
        #region ConstructorInjection
        private IBookingsRepository _bookingsRepository;
        private IMailBusiness _mailBusiness;
        private IQuotationsBusiness _quotationBusiness;

        public BookingsBusiness(IBookingsRepository bookingsRepository,IMailBusiness mailBusiness,IQuotationsBusiness quotationBusiness)
        {
            _bookingsRepository = bookingsRepository;
            _mailBusiness = mailBusiness;
            _quotationBusiness = quotationBusiness;
        }
        #endregion ConstructorInjection 

        public List<Bookings> GetCustomerBookings(int customerID,bool Ishistory)
        {
            List<Bookings> Bookingslist = null;
            try
            {
                Bookingslist = _bookingsRepository.GetCustomerBookings(customerID, Ishistory);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Bookingslist;
        }

        public OperationsStatus InsertBookings(Bookings bookingsObj)
        {
            OperationsStatus OSatObj = null;
            try
            {
                OSatObj = _bookingsRepository.InsertBookings(bookingsObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OSatObj;
        }

        public List<Bookings> GetAllBookings()
        {
            List<Bookings> Bookingslist = null;
            try
            {
                Bookingslist = _bookingsRepository.GetAllBookings();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Bookingslist;
        }

        public Bookings GetBookings(int BookingID)
        {
            Bookings bookingObj = null;
            try
            {
                bookingObj = _bookingsRepository.GetBookings(BookingID);
                if (bookingObj.ProductSpecXML != null)
                {
                    bookingObj.AttributeValues = _quotationBusiness.GetAttributeValueFromXML(bookingObj.ProductSpecXML);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return bookingObj;
        }

        public OperationsStatus UpdateBookings(Bookings bookingsObj)
        {
            OperationsStatus OSatObj = null;
            try
            {
                OSatObj = _bookingsRepository.UpdateBookings(bookingsObj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OSatObj;
        }

        public async Task<bool> BookingsEmail(Bookings bookingsObj)
        {
            bool sendsuccess = false;
            try
            {
                if (bookingsObj.customerObj.Email != "")
                {
                    Mail _mail = new Mail();
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PartyEcTemplates/Booking.html")))
                    {
                        _mail.Body = reader.ReadToEnd();
                    }
                    _mail.Body = _mail.Body.Replace("{CustomerName}", bookingsObj.customerObj.Name);
                    _mail.Body = _mail.Body.Replace("{BookingDate}", bookingsObj.BookingDate);
                    _mail.Body = _mail.Body.Replace("{ProductName}", bookingsObj.ProductName);
                    _mail.Body = _mail.Body.Replace("{BookingNo}", bookingsObj.BookingNo);
                    _mail.Body = _mail.Body.Replace("{RequiredDate}", bookingsObj.RequiredDate);
                    _mail.Body = _mail.Body.Replace("{Qty}", bookingsObj.Qty.ToString());
                    _mail.Body = _mail.Body.Replace("{Price}", bookingsObj.Price.ToString());
                    _mail.Body = _mail.Body.Replace("{tax}", bookingsObj.TaxAmt.ToString());
                    _mail.Body = _mail.Body.Replace("{additionalCharges}", bookingsObj.AdditionalCharges.ToString());
                    _mail.Body = _mail.Body.Replace("{discount}", bookingsObj.DiscountAmt.ToString());
                    _mail.Body = _mail.Body.Replace("{subTotal}", bookingsObj.SubTotal.ToString());
                    _mail.Body = _mail.Body.Replace("{grandTotal}", bookingsObj.GrandTotal.ToString());
                    _mail.Body = _mail.Body.Replace("{Status}", bookingsObj.StatusText.ToString());
                    _mail.IsBodyHtml = true;
                    _mail.Subject = "Booking No:" + bookingsObj.BookingNo;
                    _mail.To = bookingsObj.customerObj.Email;
                    sendsuccess = await _mailBusiness.MailSendAsync(_mail);
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            return sendsuccess;
        }


    }
}