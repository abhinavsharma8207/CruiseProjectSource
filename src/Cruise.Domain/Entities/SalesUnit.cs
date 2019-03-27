using System.Collections.Generic;

namespace Cruise.Domain.Entities
{
    public class SalesUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }

        public List<Ship> Ships { get; set; }
    }
}