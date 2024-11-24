using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using TimesheetSystem.UI.Controllers;
using TimesheetSystem.UI.Models;
using TimesheetSystem.UI.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TimesheetSystem.UnitTests
{
    public class HomeControllerTests
    {
        private readonly Mock<ITimesheetDataRepository> _repositoryMock;
        private readonly HomeController _controller;
        private readonly Mock<ITempDataDictionary> _tempDataMock;

        public HomeControllerTests()
        {
            _repositoryMock = new Mock<ITimesheetDataRepository>();
            _tempDataMock = new Mock<ITempDataDictionary>();
            _controller = new HomeController(_repositoryMock.Object)
            {
                TempData = _tempDataMock.Object
            };
        }

        // Test for Index Action
        [Fact]
        public void Index_WithMockData_ReturnsTimeSheetList()
        {
            // Arrange
            var mockData = new List<Timesheet>
                {
                    new Timesheet { ID = 1, UserName = "John Smith", HoursWorked = 8 },
                    new Timesheet { ID = 2, UserName = "Alice Johnson", HoursWorked = 7 }
                };
            _repositoryMock.Setup(r => r.GetAll()).Returns(mockData);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as List<Timesheet>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }
    }
}
