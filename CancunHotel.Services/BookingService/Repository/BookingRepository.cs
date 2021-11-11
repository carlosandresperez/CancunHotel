using System;
using System.Threading.Tasks;
using CancunHotel.Domain.Entities;

namespace CancunHotel.Services.BookingService.Repository
{
    public class BookingRepository: IBookingRepository
    {
        public BookingRepository()
        {
        }

        public async Task<Booking> AddReservation(Booking booking)
        {
            booking.Id = Guid.NewGuid().ToString();

            return booking;
        }

        public async Task<bool> CancelReservation(string reservationId)
        {
            return true;
        }

        public async Task<bool> CheckAvailability(DateTime startDate, DateTime endDate)
        {
            return true;
        }

        public async Task<Booking> GetBooking(string bookingId)
        {
            return new Booking() { Id = bookingId, EndDate = DateTime.Now.AddDays(2), StartDate = DateTime.Now };
        }

        public async Task<bool> ModifyReservation(Booking booking)
        {
            return true;
        }
    }
}
