using Cruise.Domain.Entities;
using Cruise.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cruise.DAL
{
    public class CruiseDataService : ICruiseDataService
    {
        private readonly CruiseDbContext _context;

        public CruiseDataService(CruiseDbContext context)
        {
            _context = context;
        }

        //service to get salesUnits list
        public IEnumerable<SalesUnitSalesData> GetSalesUnitSalesData(DateTime startDate, DateTime endDate)
        {
            return _context.SalesUnits
                .Join(_context.Ships, unit => unit.Id, ship => ship.SalesUnitId,
                    (salesUnit, ship) => new { salesUnit, ship })
                .Join(_context.Bookings, shipUnit => shipUnit.ship.Id, booking => booking.ShipId,
                    (shipUnit, booking) => new { shipUnit, booking })
                .Where(salesUnitData =>
                    salesUnitData.booking.BookingDate >= startDate && salesUnitData.booking.BookingDate <= endDate)
                .GroupBy(salesUnitData => salesUnitData.shipUnit.salesUnit.Id)
                .Select(salesUnitData => new SalesUnitSalesData
                {
                    SalesUnit = salesUnitData.First().shipUnit.salesUnit,
                    BookingTotal = salesUnitData.Sum(p => p.booking.Price)
                });
        }

        //service to get BookingDetails of a given salesUnitId
        public IEnumerable<BookingData> GetBookingsBySalesUnit(DateTime startDate, DateTime endDate, int salesUnitId)
        {
            return _context.SalesUnits
                .Join(_context.Ships, unit => unit.Id, ship => ship.SalesUnitId,
                    (salesUnit, ship) => new { salesUnit, ship })
                .Join(_context.Bookings, shipUnit => shipUnit.ship.Id, booking => booking.ShipId,
                    (shipUnit, booking) => new { shipUnit, booking })
                .Where(bookingData => bookingData.booking.BookingDate >= startDate &&
                                      bookingData.booking.BookingDate <= endDate &&
                                      bookingData.shipUnit.ship.SalesUnitId == salesUnitId)
                .Select(bookingData => new BookingData
                {
                    BookingId = bookingData.booking.Id,
                    ShipName = bookingData.shipUnit.ship.Name,
                    Price = bookingData.booking.Price,
                    Currency = bookingData.shipUnit.salesUnit.Currency
                });
        }

        //service to get BookingDetails of a given salesUnitId and search text
        public IEnumerable<BookingData> SearchBookings(DateTime startDate, DateTime endDate, int salesUnitId, string searchTerm)
        {
            var bookingsData = GetBookingsBySalesUnit(startDate, endDate, salesUnitId);

            return bookingsData.Where(booking =>
                (!string.IsNullOrEmpty(booking.ShipName) && booking.ShipName.ToLower().Contains(searchTerm.ToLower()))
                || booking.BookingId.ToString().Contains(searchTerm));
        }

        public void CreateBookings(List<Booking> bookings)
        {
            _context.Bookings.AddRange(bookings);
            _context.SaveChanges();
        }

        public void CreateShips(List<Ship> ships)
        {
            _context.Ships.AddRange(ships);
            _context.SaveChanges();
        }

        public void CreateSalesUnits(List<SalesUnit> salesUnits)
        {
            _context.SalesUnits.AddRange(salesUnits);
            _context.SaveChanges();
        }
    }
}
