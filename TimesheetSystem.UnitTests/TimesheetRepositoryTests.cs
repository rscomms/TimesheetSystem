using Moq;
using TimesheetSystem.UI.Models;
using TimesheetSystem.UI.Repositories;
using Xunit;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimesheetSystem.UnitTests
{
    public class TimesheetRepositoryTests
    {
        private readonly Mock<ITimesheetDataRepository> _repositoryMock;
        public TimesheetRepositoryTests()
        {
            _repositoryMock = new Mock<ITimesheetDataRepository>();
        }


        [Fact]
        public void Add_ValidEntry_CallsRepositoryAddOnce()
        {
            // Arrange
            var newEntry = new Timesheet { ID = 3, UserName = "Jane Doe", HoursWorked = 5 };

            // Act
            _repositoryMock.Object.Add(newEntry);

            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<Timesheet>()), Times.Once);
        }

        [Fact]
        public void GetAll_ValidEntry_ReturnsNoEntries()
        {
            // Arrange
            var newEntry = new Timesheet { ID = 3, UserName = "Jane Doe", HoursWorked = 5 };

            // Act
            _repositoryMock.Object.Add(newEntry);

            // Assert
            Assert.False(_repositoryMock.Object.GetAll().Count() > 0);
        }

        [Fact]
        //Returns list of entries using the mock data
        public void GetAll_MockEntry_ReturnsListOfEntries()
        {
            // Arrange
            var mockData = new List<Timesheet>
            {
                new Timesheet { ID = 1, UserName = "John Smith", HoursWorked = 8 },
                new Timesheet { ID = 2, UserName = "Alice Johnson", HoursWorked = 7 }
            };
            _repositoryMock.Setup(r => r.GetAll()).Returns(mockData);

            // Act
            var result = _repositoryMock.Object.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, e => e.UserName == "John Smith");
        }

        [Fact]
        //Returns list of entries from the in-memory database - successfully
        public void GetAll_ReturnsListOfEntries()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TimesheetDb")
                .Options;
            var context = new ApplicationDbContext(options);

            TimesheetDataRepository Object = new TimesheetDataRepository(context);

            var newEntry = new Timesheet { ID = 3, UserName = "Jane Doe", ProjectName = "Project A", TaskDesc = "Feature A", HoursWorked = 5 };
            Object.Add(newEntry);

            // Act
            var result = Object.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, e => e.UserName == "Jane Doe");
        }


        //We can add similar test cases for GetRemainingHours and UpdateWorkingHours and also may be we add failed cases as well.
    }
}
