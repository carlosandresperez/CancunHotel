using System;
using System.Threading.Tasks;
using CancunHotel.Domain.Entities;
using CancunHotel.Services.BookingService.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace CancunHotel.API.Controllers
{
    public class BookingController: Controller
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpPost("addReservation")]
        public async Task<IActionResult> AddReservation([FromBody]Booking booking)
        {
            return Ok(await bookingService.AddReservation(booking));
        }

        [HttpPatch("cancelReservation")]
        public async Task<IActionResult> CancelReservation(string reservationId)
        {
            return Ok(await bookingService.CancelReservation(reservationId));
        }

        [HttpPut("ModifyReservation")]
        public async Task<IActionResult> ModifyReservation([FromBody]Booking booking)
        {
            return Ok(await bookingService.ModifyReservation(booking));
        }

        [HttpGet("CheckAvailability")]
        public async Task<IActionResult> CheckAvailability(DateTime startDate, DateTime endDate)
        {
            return Ok(await bookingService.CheckAvailability(startDate, endDate));
        }
    }
}
