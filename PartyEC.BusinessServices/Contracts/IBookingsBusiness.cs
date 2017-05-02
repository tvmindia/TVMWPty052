using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.BusinessServices.Contracts
{
    public interface IBookingsBusiness
    {
        List<Bookings> GetCustomerBookings(int customerID,bool Ishistory);
        OperationsStatus InsertBookings(Bookings bookingsObj);
        List<Bookings> GetAllBookings();
        Task<bool> BookingsEmail(Bookings bookingsObj);
        Bookings GetBookings(int BookingID);
        OperationsStatus UpdateBookings(Bookings bookingsObj);
    }
}
