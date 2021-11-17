using System;
namespace CancunHotel.Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
        }

        public string Id { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
