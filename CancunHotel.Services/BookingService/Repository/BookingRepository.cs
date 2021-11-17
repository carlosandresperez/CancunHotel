using System;
using System.Threading.Tasks;
using CancunHotel.Data.Context;
using CancunHotel.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CancunHotel.Services.BookingService.Repository
{
    public class BookingRepository: IBookingRepository
    {
        private readonly DataContext dbContext;
        public BookingRepository(DataContext dataContext)
        {
            this.dbContext = dataContext;
        }

        public async Task<Booking> AddReservation(Booking booking)
        {
            booking.Id = Guid.NewGuid().ToString();
            booking.Status = true;
            dbContext.Booking.Add(booking);
            await dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> CancelReservation(string reservationId)
        {
            var booking = dbContext.Booking.FirstOrDefault(b => b.Id == reservationId);
            booking.Status = false;
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> CheckAvailability(DateTime startDate, DateTime endDate)
        {
            var res = await dbContext.Booking.Where(b => ((b.StartDate >= startDate && b.StartDate <= endDate) || (b.EndDate >= startDate && b.EndDate <= endDate)) && b.Status == true).ToListAsync();
            if (res.Count > 0) return false;
            return true;
        }

        public async Task<bool> CheckAvailabilityToModify(DateTime startDate, DateTime endDate, string bookingId)
        {
            var res = await dbContext.Booking.Where(b => ((b.StartDate >= startDate && b.StartDate <= endDate) || (b.EndDate >= startDate && b.EndDate <= endDate)) && b.Status == true && b.Id != bookingId).ToListAsync();
            if (res.Count > 0) return false;
            return true;
        }

        public async Task<Booking> GetBooking(string bookingId)
        {
            return await dbContext.Booking.Where(b => b.Id == bookingId).FirstOrDefaultAsync();
        }

        public async Task<bool> ModifyReservation(Booking booking)
        {
            var book = dbContext.Booking.FirstOrDefault(b => b.Id == booking.Id);
            book.Status = booking.Status;
            book.EndDate = booking.EndDate;
            book.StartDate = booking.StartDate;
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
