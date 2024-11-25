using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TimesheetSystem.UI.Models
{
    public class Timesheet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Marks the field as auto-incrementing
        public int ID { get; set; }
        //TO DO: Can be replaced this with UserID and linked to Users table (Not required for this demo)
        //[Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        public DateOnly Date { get; set; }
        //TO DO: Can be replaced this with ProjectID and linked to Projects table (Not required for this demo)
        public string ProjectName { get; set; }
        public string TaskDesc { get; set; }

        public int HoursWorked { get; set; }
        public int TotalHoursWorked { get; set; }
    }
}
