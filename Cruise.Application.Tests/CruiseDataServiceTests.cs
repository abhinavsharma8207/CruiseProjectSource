using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using Cruise.DAL;
using Cruise.Domain.Entities;

namespace Cruise.Application.Tests
{
    public class CruiseDataServiceTests
    {

        private readonly Mock<DbSet<Ship>> _mockShipSet;
        private readonly Mock<DbSet<Booking>> _mockBookingSet;
        private readonly Mock<DbSet<SalesUnit>> _mockSalesUnitSet;
        private readonly Mock<CruiseDbContext> _mockContext;
        private CruiseDataService _sut;
        private readonly MockCruiseDataService _mockCruiseDataService;

        public CruiseDataServiceTests()
        {
            _mockCruiseDataService = new MockCruiseDataService();
            var ships = _mockCruiseDataService.InMemoryShipsData.AsQueryable();
            var bookings = _mockCruiseDataService.InMemoryBookingsData.AsQueryable();
            var salesUnits = _mockCruiseDataService.InMemorySalesUnitData.AsQueryable();


            _mockShipSet = new Mock<DbSet<Ship>>();
            _mockBookingSet = new Mock<DbSet<Booking>>();
            _mockSalesUnitSet = new Mock<DbSet<SalesUnit>>();

            _mockShipSet.As<IQueryable<Ship>>().Setup(m => m.Provider).Returns(ships.Provider);
            _mockShipSet.As<IQueryable<Ship>>().Setup(m => m.Expression).Returns(ships.Expression);
            _mockShipSet.As<IQueryable<Ship>>().Setup(m => m.ElementType).Returns(ships.ElementType);
            _mockShipSet.As<IQueryable<Ship>>().Setup(m => m.GetEnumerator()).Returns(ships.GetEnumerator());

            _mockBookingSet.As<IQueryable<Booking>>().Setup(m => m.Provider).Returns(bookings.Provider);
            _mockBookingSet.As<IQueryable<Booking>>().Setup(m => m.Expression).Returns(bookings.Expression);
            _mockBookingSet.As<IQueryable<Booking>>().Setup(m => m.ElementType).Returns(bookings.ElementType);
            _mockBookingSet.As<IQueryable<Booking>>().Setup(m => m.GetEnumerator()).Returns(bookings.GetEnumerator());

            _mockSalesUnitSet.As<IQueryable<SalesUnit>>().Setup(m => m.Provider).Returns(salesUnits.Provider);
            _mockSalesUnitSet.As<IQueryable<SalesUnit>>().Setup(m => m.Expression).Returns(salesUnits.Expression);
            _mockSalesUnitSet.As<IQueryable<SalesUnit>>().Setup(m => m.ElementType).Returns(salesUnits.ElementType);
            _mockSalesUnitSet.As<IQueryable<SalesUnit>>().Setup(m => m.GetEnumerator()).Returns(salesUnits.GetEnumerator());

            _mockContext = new Mock<CruiseDbContext>();
            _mockContext.Setup(c => c.Ships).Returns(_mockShipSet.Object);
            _mockContext.Setup(c => c.Bookings).Returns(_mockBookingSet.Object);
            _mockContext.Setup(c => c.SalesUnits).Returns(_mockSalesUnitSet.Object);

            _sut = new CruiseDataService(_mockContext.Object);


        }

        [Fact]
        public void CreateShips_Should_AddShip()
        {
            var ships = _mockCruiseDataService.InMemoryShipsData;
            _sut.CreateShips(ships);
            _mockShipSet.Verify(m => m.AddRange(It.IsAny<List<Ship>>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CreateBookings_Should_AddBookings()
        {
            var bookings = _mockCruiseDataService.InMemoryBookingsData;
            _sut.CreateBookings(bookings);
            _mockBookingSet.Verify(m => m.AddRange(It.IsAny<List<Booking>>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void SearchBookings_Should_Return_CorrectNumberOfBookings()
        {
            var startDate = DateTimeOffset.Parse("2015-07-24T00:00:00Z").UtcDateTime;
            var endDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime;
            Assert.Equal(2, _sut.SearchBookings(startDate, endDate, 7, "").Count());

        }

        [Fact]
        public void GetSalesUnitSalesData_Should_Return_CorrectNumberOfSales()
        {
            var startDate = DateTimeOffset.Parse("2015-07-24T00:00:00Z").UtcDateTime;
            var endDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime;
            Assert.Equal(3, _sut.GetSalesUnitSalesData(startDate, endDate).Count());

        }

        [Fact]
        public void GetBookingsBySalesUnit_Should_Return_CorrectNumberOfBookingsBySalesUnit()
        {
            var startDate = DateTimeOffset.Parse("2015-07-24T00:00:00Z").UtcDateTime;
            var endDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime;
            Assert.Equal(2, _sut.GetBookingsBySalesUnit(startDate, endDate, 7).Count());

        }


    }
}
