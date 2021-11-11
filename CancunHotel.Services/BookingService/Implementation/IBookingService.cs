using System;
using System.Threading.Tasks;
using CancunHotel.Domain.Entities;
using CancunHotel.Shared.Common;

namespace CancunHotel.Services.BookingService.Implementation
{
    public interface IBookingService
    {
        Task<Response<string>> CheckAvailability(DateTime startDate, DateTime endDate);
        Task<Response<Booking>> AddReservation(Booking booking);
        Task<Response<string>> ModifyReservation(Booking booking);
        Task<Response<string>> CancelReservation(string reservationId);
    }
}
