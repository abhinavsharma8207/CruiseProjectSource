using Cruise.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cruise.DAL
{
    public class CruiseDbContext : DbContext
    {
        public CruiseDbContext(DbContextOptions<CruiseDbContext> options)
            : base(options)
        { }

        public CruiseDbContext()
        {

        }

        public virtual DbSet<SalesUnit> SalesUnits { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
    }
}
