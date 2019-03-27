using Cruise.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruise.DAL;
using Cruise.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Cruise.Application.Tests
{
    public class CruiseControllerTests
    {
        private readonly Mock<ICruiseDataService> _mockCruiseDataService;
        private readonly CruiseController _sut;

        public CruiseControllerTests()
        {
            _mockCruiseDataService = new Mock<ICruiseDataService>();

            var mockedSalesUnitData = Enumerable.Empty<SalesUnitSalesData>();

            _mockCruiseDataService.Setup(repo => repo.GetSalesUnitSalesData(It.IsAny<DateTime>(),It.IsAny<DateTime>())).Returns(mockedSalesUnitData);

            _sut = new CruiseController(_mockCruiseDataService.Object);

        }

        [Fact]
        public void GetSalesUnitSalesData_GetAction_Should_Return_Empty_List_Of_SalesUnitSalesData()
        {
            var startDate = DateTimeOffset.Parse("2015-04-24T00:00:00Z").UtcDateTime;
            var endDate = DateTimeOffset.Parse("2015-06-24T00:00:00Z").UtcDateTime;
            var result = _sut.GetSalesUnitSalesData(startDate, endDate).Result;
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult resultValue = result as OkObjectResult;
            var resultCollection = (IEnumerable<SalesUnitSalesData>)resultValue.Value;
            Assert.Empty(resultCollection);
        }

        [Fact]
        public void GetSalesUnitSalesData_GetAction__Should_Return_CorrectNumberOfSales()
        {
            var startDate = DateTimeOffset.Parse("2015-07-24T00:00:00Z").UtcDateTime;
            var endDate = DateTimeOffset.Parse("2016-05-09T00:00:00Z").UtcDateTime;
            var mockedSalesUnitData = new MockCruiseDataService().GetSalesUnitSalesData(startDate, endDate);
            _mockCruiseDataService.Setup(repo => repo.GetSalesUnitSalesData(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(mockedSalesUnitData);
            var result = _sut.GetSalesUnitSalesData(startDate, endDate).Result;
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult resultValue = result as OkObjectResult;
            var resultCollection = resultValue.Value as IEnumerable<SalesUnitSalesData>;
            Assert.Equal(3, resultCollection.Count());
        }

       


    }


}
