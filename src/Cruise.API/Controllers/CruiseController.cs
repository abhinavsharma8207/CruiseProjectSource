using Cruise.DAL;
using Cruise.Domain.Entities;
using Cruise.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cruise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CruiseController : ControllerBase
    {
        private readonly ICruiseDataService _cruiseDataService;

        public CruiseController(ICruiseDataService cruiseDataService)
        {
            _cruiseDataService = cruiseDataService;
        }

        [Route("GetSalesUnitSalesData")]
        public ActionResult<IEnumerable<SalesUnitSalesData>> GetSalesUnitSalesData(DateTime startDate, DateTime endDate)
        {
            var result = _cruiseDataService.GetSalesUnitSalesData(startDate, endDate);
           
            return Ok(result);
        }

        [Route("GetBookings")]
        public ActionResult<IEnumerable<BookingData>> GetBookingsBySalesUnit(DateTime startDate, DateTime endDate, int salesUnitId)
        {
            var result = _cruiseDataService.GetBookingsBySalesUnit(startDate, endDate, salesUnitId);

            return Ok(result);
        }

        [Route("SearchBookings")]
        public ActionResult<IEnumerable<BookingData>> SearchBookings(DateTime startDate, DateTime endDate, int salesUnitId, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var bookingsData = _cruiseDataService.GetBookingsBySalesUnit(startDate, endDate, salesUnitId);
                return Ok(bookingsData);
            }

            var bookingsSearchData = _cruiseDataService.SearchBookings(startDate, endDate, salesUnitId, searchTerm);
            return Ok(bookingsSearchData);
        }

        [HttpPost]
        [Route("Bookings")]
        public ActionResult CreateBookings(List<Booking> bookings)
        {
            _cruiseDataService.CreateBookings(bookings);

            return NoContent();
        }

        [HttpPost]
        [Route("Ships")]
        public ActionResult CreateShips(List<Ship> ships)
        {
            _cruiseDataService.CreateShips(ships);

            return NoContent();
        }

        [HttpPost]
        [Route("SalesUnits")]
        public ActionResult CreateSalesUnits(List<SalesUnit> salesUnits)
        {
            _cruiseDataService.CreateSalesUnits(salesUnits);

            return NoContent();
        }
    }
}
