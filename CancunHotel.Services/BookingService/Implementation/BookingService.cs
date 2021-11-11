using System;
using System.Threading.Tasks;
using CancunHotel.Domain.Entities;
using CancunHotel.Services.BookingService.Repository;
using CancunHotel.Shared.Common;

namespace CancunHotel.Services.BookingService.Implementation
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public async Task<Response<Booking>> AddReservation(Booking booking)
        {
            try
            {
                //Validate data
                var validation = ValidateAvailability(booking.StartDate, booking.EndDate);
                if (validation.Success)
                {
                    var result = await bookingRepository.AddReservation(booking);
                    return Response<Booking>.CreateSuccessfulResponse<Response<Booking>>(result);
                }
                else
                {
                    return Response<Booking>.CreateFailedResponse<Response<Booking>>(validation.Message);
                }
            }
            catch (Exception)
            {
                //LogError
                return Response<Booking>.CreateFailedResponse<Response<Booking>>();
            }
        }

        public async Task<Response<string>> CancelReservation(string reservationId)
        {
            try
            {
                var message = "";
                var result = await bookingRepository.CancelReservation(reservationId);
                if (result)
                {
                    message = $"Reservation {reservationId} canceled";
                }
                else
                {
                    message = $"Coult not cancel the reservation {reservationId}";
                }
                return Response<string>.CreateSuccessfulResponse<Response<string>>(message);
            }
            catch (Exception)
            {
                //LogError
                return Response<string>.CreateFailedResponse<Response<string>>();
            }
        }

        public async Task<Response<string>> CheckAvailability(DateTime startDate, DateTime endDate)
        {
            try
            {
                var validation = ValidateAvailability(startDate, endDate);
                if (validation.Success)
                {
                    var result = await bookingRepository.CheckAvailability(startDate, endDate);
                    if (!result)
                    {
                        return Response<string>.CreateFailedResponse<Response<string>>("The range of dates selected are not available.");
                    }
                }
                return validation;
            }
            catch (Exception)
            {
                //LogError
                return Response<string>.CreateFailedResponse<Response<string>>();
            }
        }

        public async Task<Response<string>> ModifyReservation(Booking booking)
        {
            try
            {
                var message = "";
                var result = await bookingRepository.ModifyReservation(booking);
                if (result)
                {
                    message = $"Reservation {booking.Id} modified";
                }
                else
                {
                    message = $"Coult not modify the reservation {booking.Id}";
                }
                return Response<string>.CreateSuccessfulResponse<Response<string>>(message);
            }
            catch (Exception)
            {
                //LogError
                return Response<string>.CreateFailedResponse<Response<string>>();
            }
        }

        private Response<string> ValidateAvailability(DateTime startDate, DateTime endDate)
        {
            var bookingDays = (endDate - startDate).TotalDays;
            if (bookingDays > 3)
            {
                return Response<string>.CreateFailedResponse<Response<string>>("Cannot book more than 3 days");
            }

            var currentDate = DateTime.Now;
            var daysBeforeReserve = (startDate.Date - currentDate.Date).TotalDays;
            if (daysBeforeReserve > 30)
            {
                return Response<string>.CreateFailedResponse<Response<string>>("Cannot reserve more than 30 days in advance");
            }

            return Response<string>.CreateSuccessfulResponse<Response<string>>("The range of dates selected are valid.");
        }
    }
}
