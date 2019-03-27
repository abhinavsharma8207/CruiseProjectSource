using System;
using System.Collections.Generic;
using Cruise.Domain.Entities;
using Cruise.Domain.ViewModels;

namespace Cruise.DAL
{
    public interface ICruiseDataService
    {
        IEnumerable<SalesUnitSalesData> GetSalesUnitSalesData(DateTime startDate, DateTime endDate);
        IEnumerable<BookingData> GetBookingsBySalesUnit(DateTime startDate, DateTime endDate, int salesUnitId);

        IEnumerable<BookingData> SearchBookings(DateTime startDate, DateTime endDate, int salesUnitId, string searchTerm);
        void CreateBookings(List<Booking> bookings);
        void CreateShips(List<Ship> ships);
        void CreateSalesUnits(List<SalesUnit> salesUnits);
    }
}