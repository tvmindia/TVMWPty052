using PartyEC.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyEC.RepositoryServices.Contracts
{
    public interface IBookingsRepository
    {
        List<Bookings> GetCustomerBookings(int customerID,bool Ishistory);
        OperationsStatus InsertBookings(Bookings bookingsObj);
        List<Bookings> GetAllBookings();
        Bookings GetBookings(int BookingID);
        OperationsStatus UpdateBookings(Bookings bookingsObj);
    }
}
