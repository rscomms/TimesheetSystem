namespace TimesheetSystem.UI.Models
{
    public class Timesheet
    {
        public int ID { get; set; }
        //TO DO: replace this with UserID later (Not required for this demo)
        public string UserName { get; set; }
        public DateOnly Date { get; set; }
        //TO DO: replace this with ProjectID
        public string ProjectName { get; set; }
        public string TaskDesc { get; set; }

        public int HoursWorked { get; set; }
        public int TotalHoursWorked { get; set; }
    }
}
