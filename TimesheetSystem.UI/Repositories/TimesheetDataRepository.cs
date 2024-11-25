using TimesheetSystem.UI.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimesheetSystem.UI.Repositories
{
    public class TimesheetDataRepository : ITimesheetDataRepository
    {
        private readonly ApplicationDbContext _context;
        //TO DO: Error logging

        public TimesheetDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Timesheet entry)
        {
            _context.Timesheets.Add(entry);
            _context.SaveChanges();
        }
    

        public IEnumerable<Timesheet> GetAll()
        {
            //return _context.Timesheets.ToList();
            return _context.Timesheets
                .OrderByDescending(entry => entry.Date) // First order by entryDate
                .ThenBy(entry => entry.UserName) // Then order by userName
                .ToList();
        }

        public int GetTotalHoursWorked(string userName, DateOnly entryDate)
        {
            int totalHoursWorked = _context.Timesheets
                .Where(entry => entry.UserName == userName && entry.Date == entryDate)
                .Sum(entry => entry.HoursWorked);

            return totalHoursWorked;
        }

        public void UpdateWorkingHours(string userName, DateOnly entryDate)
        {
            //for each record with userName and entryDate
            //update totalWorkingHours as sum of all workingHours()

            var entries = _context.Timesheets
            .Where(entry => entry.UserName == userName && entry.Date == entryDate)
            .ToList();

            int totalWorkedHours = entries.Sum(entry => entry.HoursWorked);

            foreach (var entry in entries)
            {
                entry.TotalHoursWorked = totalWorkedHours;
            }

            _context.SaveChanges();
        }
    }
}
