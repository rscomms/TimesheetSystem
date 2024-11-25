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
        public void Add_WithMockData_ReturnsTimeSheetList()
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
            Assert.NotNull(result);//Checks if view result is not NULL
            var model = result.Model as List<Timesheet>;
            Assert.NotNull(model); //Checks if the returned model is not NULL
            Assert.Equal(2, model.Count);//Checks if the returned model list count is 2
		}


        // Test for Add method when hoursWorked is greater than remainingHours, should fail to add timesheet
        [Fact]
        public void Add_EntryWithExcessiveHours_FailedToAddTimeSheet()
        {
            // Arrange
            var newEntry = new Timesheet
            {
                UserName = "Test User",
                Date = new DateOnly(2024, 11, 24),
                ProjectName = "Test Project",
                TaskDesc = "Exceeds available hours",
                HoursWorked = 10  // Entry with hours exceeding remaining
            };

            // Mock total hours to a valid value (e.g., 5 hours)
            _repositoryMock.Setup(r => r.GetTotalHoursWorked(It.IsAny<string>(), It.IsAny<DateOnly>())).Returns(5);

            // Act
            var result = _controller.Add(newEntry) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Redirected to Index
            _repositoryMock.Verify(r => r.Add(It.IsAny<Timesheet>()), Times.Never);  // Ensure Add is not called
            _repositoryMock.Verify(r => r.UpdateWorkingHours(It.IsAny<string>(), It.IsAny<DateOnly>()), Times.Never);  // Ensure updateWorkingHours is not called
        }

		// Test for Add method when remainingHours is 0, should fail to add timesheet
		[Fact]
        public void Add_EntryWithNoRemainingHours_FailedToAddTimeSheet()
        {
            // Arrange
            var newEntry = new Timesheet
            {
                UserName = "Test User",
                Date = new DateOnly(2024, 11, 24),
                ProjectName = "Test Project",
                TaskDesc = "No remaining hours",
                HoursWorked = 5  // Entry with hours
            };

			// Mock total hours to a valid value (e.g., 6 hours)
			_repositoryMock.Setup(r => r.GetTotalHoursWorked(It.IsAny<string>(), It.IsAny<DateOnly>())).Returns(6);

            // Act
            var result = _controller.Add(newEntry) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Redirected to Index
            _repositoryMock.Verify(r => r.Add(It.IsAny<Timesheet>()), Times.Never);  // Ensure Add is not called
            _repositoryMock.Verify(r => r.UpdateWorkingHours(It.IsAny<string>(), It.IsAny<DateOnly>()), Times.Never);  // Ensure updateWorkingHours is not called
        }

        // Test for Add method when everything is valid, should add entry and update working hours successfully
        [Fact]
        public void Add_ValidEntry_AddsEntryAndUpdatesWorkingHours()
        {
            // Arrange
            var newEntry = new Timesheet
            {
                UserName = "Test User",
                Date = new DateOnly(2024, 11, 24),
                ProjectName = "Test Project",
                TaskDesc = "Valid entry",
                HoursWorked = 4
            };

			// Mock total hours to a valid value (e.g., 2 hours)
			_repositoryMock.Setup(r => r.GetTotalHoursWorked(It.IsAny<string>(), It.IsAny<DateOnly>())).Returns(2);

            // Act
            var result = _controller.Add(newEntry) as RedirectToActionResult;

            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<Timesheet>()), Times.Once); // Ensure Add is called once
            _repositoryMock.Verify(r => r.UpdateWorkingHours(It.IsAny<string>(), It.IsAny<DateOnly>()), Times.Once); // Ensure updateWorkingHours is called once
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Redirected to Index
        }
    }
}
