using PartyEC.BusinessServices.Contracts;
using PartyEC.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyEC.DataAccessObject.DTO;

namespace PartyEC.BusinessServices.Services
{
    public class BookingsBusiness: IBookingsBusiness
    {
        #region ConstructorInjection

        private IBookingsRepository _bookingsRepository;

        public BookingsBusiness(IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository;
        }
        #endregion ConstructorInjection 

        public List<Bookings> GetCustomerBookings(int customerID,bool Ishistory)
        {
            List<Bookings> Bookingslist = null;
            try
            {
                Bookingslist = _bookingsRepository.GetCustomerBookings(customerID, Ishistory);

            }
            catch (Exception)
            {

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
            catch (Exception)
            {

            }
            return OSatObj;
        }
    }
}