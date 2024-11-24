using System.Collections.Generic;
using TimesheetSystem.UI.Models;

namespace TimesheetSystem.UI.Repositories
{
    public interface ITimesheetDataRepository
    {
        void Add(Timesheet entry);
        IEnumerable<Timesheet> GetAll();
        int getRemainingHours(string userName, DateOnly entryDate);
        void updateWorkingHours(string userName, DateOnly entryDate);
    }
}
