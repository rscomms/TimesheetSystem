namespace TimesheetSystem.UI.Models
{
    public class Timesheet
    {
        public int EntryID { get; set; }
        public string EmployeeName { get; set; }
        public DateOnly EntryDate { get; set; }

        public string ProjectName { get; set; }
        public string Description { get; set; }

        public int HoursWorked { get; set; }
    }
}
