using System;

namespace Cruise.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int ShipId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal Price { get; set; }
    }
}