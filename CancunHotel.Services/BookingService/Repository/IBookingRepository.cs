using System;
using System.Threading.Tasks;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Services.BookingService.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> GetBooking(string bookingId);
        Task<bool> CheckAvailability(DateTime startDate, DateTime endDate);
        Task<Booking> AddReservation(Booking booking);
        Task<bool> ModifyReservation(Booking booking);
        Task<bool> CancelReservation(string reservationId);
    }
}
