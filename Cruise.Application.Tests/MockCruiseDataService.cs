using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruise.DAL;
using Cruise.Domain.Entities;
using Cruise.Domain.ViewModels;

namespace Cruise.Application.Tests
{
    public class MockCruiseDataService : ICruiseDataService
    {

        public readonly List<Ship> InMemoryShipsData;

        public readonly List<Booking> InMemoryBookingsData;

        public readonly List<SalesUnit> InMemorySalesUnitData;

        public MockCruiseDataService()
        {
            InMemoryShipsData = new List<Ship>
            {
                new Ship
                {
                    Id = 1,
                    SalesUnitId = 7,
                    Name = "Aegean Paradise"
                },
                new Ship
                {
                    Id = 2,
                    SalesUnitId = 7,
                    Name = "AIDAaura"
                },

                new Ship
                {
                    Id = 3,
                    SalesUnitId = 2,
                    Name = "AIDAbella"
                   
                },

                new Ship
                {
                    Id = 4,
                    SalesUnitId = 2,
                    Name = "AIDAblu"
                },
                new Ship
                {
                    Id =5,
                    SalesUnitId = 3,
                    Name = "AIDAcara"
                },

                new Ship
                {
                    Id =6,
                    SalesUnitId =5,
                    Name ="AIDAdiva"
                },

                new Ship
                {
                    Id = 7,
                    SalesUnitId = 1,
                    Name ="AIDAluna"
                },

                new Ship
                {
                    Id = 8,
                    SalesUnitId = 6,
                    Name ="AIDAmar"
                },

                new Ship
                {
                    Id = 9,
                    SalesUnitId = 8,
                    Name = "AIDAprima"
                },

                new Ship
                {
                    Id =10,
                    SalesUnitId = 8,
                    Name ="AIDAsol"
                },
                new Ship
                {
                    Id =11,
                    SalesUnitId = 2,
                    Name = "AIDAstella"
                },
                new Ship
                {
                    Id =12,
                    SalesUnitId =2,
                    Name ="AIDAvita"
                },

                new Ship
                {
                    Id =13,
                    SalesUnitId =3,
                    Name ="Allure of the Seas"
                },
                new Ship
                {
                    Id =14,
                    SalesUnitId =8,
                    Name ="American Eagle"
                }

            };

            InMemoryBookingsData = new List<Booking>
            {
               
                new Booking
                {
                    Id = 1,
                    ShipId = 219,
                    BookingDate = DateTimeOffset.Parse("2015-07-24T00:00:00Z").UtcDateTime,
                    Price = Convert.ToDecimal(6080.77)
                },

                new Booking
                {
                    Id = 2,
                    ShipId = 141,
                    BookingDate = DateTimeOffset.Parse("2015-07-24T00:00:00Z").UtcDateTime,
                    Price = Convert.ToDecimal(6205.11)
                },
                new Booking
                {
                    Id = 3,
                    ShipId = 200,
                    BookingDate = DateTimeOffset.Parse("2015-10-10T00:00:00Z").UtcDateTime,
                    Price = Convert.ToDecimal(8042.63)
                },

                new Booking
                {
                    Id = 4,
                    ShipId = 257,
                    BookingDate = DateTimeOffset.Parse("2016-03-22T00:00:00Z").UtcDateTime,
                    Price = Convert.ToDecimal(2703.24)
                },

                new Booking
                {
                    Id = 5,
                    ShipId = 112,
                    BookingDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime,
                    Price = Convert.ToDecimal(9381.10)
                },

                new Booking
                {
                Id = 6,
                ShipId = 1,
                BookingDate = DateTimeOffset.Parse("2015-07-24T00:00:00Z").UtcDateTime,
                Price = Convert.ToDecimal(6080.77)
                },

                new Booking
                {
                Id = 7,
                ShipId = 1,
                BookingDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime,
                Price = Convert.ToDecimal(6080.77)
            },

                new Booking
                {
                    Id = 8,
                    ShipId = 3,
                    BookingDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime,
                    Price = Convert.ToDecimal(6080.77)
                },

                new Booking
                {
                Id = 9,
                ShipId = 5,
                BookingDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime,
                Price = Convert.ToDecimal(6080.77)
            }

            };

            InMemorySalesUnitData = new List<SalesUnit>
            {
                new SalesUnit
                {
                Id =1,
                Name ="dreamlines.de",
                Country = "Germany",
                Currency = "€"
            },
                new SalesUnit
            {
                Id =2,
                Name ="dreamlines.com.br",
                Country = "Brazil",
                Currency = "R$"
            },
                new SalesUnit
            {
                Id =3,
                Name ="dreamlines.it",
                Country = "Italy",
                Currency = "€"
            },
                new SalesUnit
            {
                Id =4,
                Name ="dreamlines.fr",
                Country = "France",
                Currency = "€"
            },

                new SalesUnit
            {
                Id = 5,
                Name ="dreamlines.com.au",
                Country = "Australia",
                Currency = "AU$"
            },
                new SalesUnit
            {
                Id = 6,
                Name ="dreamlines.ru",
                Country = "Russia",
                Currency = "RUB"
            },
                new SalesUnit
            {
                Id = 7,
                Name ="dreamlines.nl",
                Country ="Netherlands",
                Currency = "€"
            },
                new SalesUnit
            {
                 Id =8,
                Name ="soyoulun.com",
                Country ="China",
                Currency = "¥"
            }
            


            };


        }

        public void CreateBookings(List<Booking> bookings)
        {
            throw new NotImplementedException();
        }

        public void CreateSalesUnits(List<SalesUnit> salesUnits)
        {
            throw new NotImplementedException();
        }

        public void CreateShips(List<Ship> ships)
        {
            throw new NotImplementedException();

        }

        public IEnumerable<BookingData> GetBookingsBySalesUnit(DateTime startDate, DateTime endDate, int salesUnitId)
        {
            var ret = InMemorySalesUnitData
                .Join(InMemoryShipsData, unit => unit.Id, ship => ship.SalesUnitId,
                    (salesUnit, ship) => new { salesUnit, ship })
                .Join(InMemoryBookingsData, shipUnit => shipUnit.ship.Id, booking => booking.ShipId,
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
            return ret;

        }

        public IEnumerable<SalesUnitSalesData> GetSalesUnitSalesData(DateTime startDate, DateTime endDate)
        {
            return InMemorySalesUnitData
                .Join(InMemoryShipsData, unit => unit.Id, ship => ship.SalesUnitId,
                    (salesUnit, ship) => new {salesUnit, ship})
                .Join(InMemoryBookingsData, shipUnit => shipUnit.ship.Id, booking => booking.ShipId,
                    (shipUnit, booking) => new {shipUnit, booking})
                .Where(salesUnitData =>
                    salesUnitData.booking.BookingDate >= startDate && salesUnitData.booking.BookingDate <= endDate)
                .GroupBy(salesUnitData => salesUnitData.shipUnit.salesUnit.Id)
                .Select(salesUnitData => new SalesUnitSalesData
                {
                    SalesUnit = salesUnitData.First().shipUnit.salesUnit,
                    BookingTotal = salesUnitData.Sum(p => p.booking.Price)
                });
        }

        public IEnumerable<BookingData> SearchBookings(DateTime startDate, DateTime endDate, int salesUnitId, string searchTerm)
        {
            var bookingsData = GetBookingsBySalesUnit(startDate, endDate, salesUnitId);

            return bookingsData.Where(booking =>
                (!string.IsNullOrEmpty(booking.ShipName) && booking.ShipName.ToLower().Contains(searchTerm.ToLower()))
                || booking.BookingId.ToString().Contains(searchTerm));
        }
    }
}
