using Microsoft.EntityFrameworkCore;
using Xunit;
using TimesheetSystem.UI.Models;
using System.Linq;

namespace TimesheetSystem.UnitTests
{
    public class ApplicationDbContextTests
    {
        [Fact]
        public void Add_MockDataToInMemoryDb_UsingDbContext_ReturnsInsertedData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TimesheetDb")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var newEntry = new Timesheet
                {
                    UserName = "Jane Doe",
                    Date = new DateOnly(2024, 11, 24),
                    ProjectName = "Sample Project",
                    TaskDesc = "Worked on unit tests",
                    HoursWorked = 5,
                    TotalHoursWorked = 5
                };

                context.Timesheets.Add(newEntry);
                context.SaveChanges();
            }

            // Verify the entry was saved
            using (var context = new ApplicationDbContext(options))
            {
                var entry = context.Timesheets.FirstOrDefault(e => e.UserName == "Jane Doe");
                Assert.NotNull(entry);
                Assert.Equal("Jane Doe", entry.UserName);
                Assert.Equal(5, entry.HoursWorked);
            }
        }
    }
}