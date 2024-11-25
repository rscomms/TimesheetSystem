using System.Collections.Generic;
using TimesheetSystem.UI.Models;

namespace TimesheetSystem.UI.Repositories
{
    public interface ITimesheetDataRepository
    {
        void Add(Timesheet entry);
        IEnumerable<Timesheet> GetAll();
        int GetTotalHoursWorked(string userName, DateOnly entryDate);
        void UpdateWorkingHours(string userName, DateOnly entryDate);
    }
}
