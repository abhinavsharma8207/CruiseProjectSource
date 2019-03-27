using System.Collections.Generic;

namespace Cruise.Domain.Entities
{
    public class Ship
    {
        public int Id { get; set; }
        public int SalesUnitId { get; set; }
        public string Name { get; set; }
        
        public List<Booking> Bookings { get; set; }
    }
}