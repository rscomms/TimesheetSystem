using TimesheetSystem.UI.Models;

namespace TimesheetSystem.UI.Util
{
    public class GenerateMockData
    {
        public static List<Timesheet> MockTimesheetEntryModels()
        {
            List<Timesheet> res = new();

            string[] userNames = { "John Smith", "Alice Johnson", "Michael Brown", "Emily Davis", "David Wilson" };
            string[] projectNames = { "Todo App", "Bug Tracker", "E-commerce Site", "Blog Platform", "Chat App" };
            string[] taskDescs = {
                    "Implemented CRUD",
                    "Fixed Bugs",
                    "Developed UI",
                    "Added Authentication",
                    "Enhanced Performance"
                };

            Random random = new Random();
            Dictionary<(string userName, DateOnly date), List<Timesheet>> dailyEntries = new(); // Tracks daily entries for each employee

            DateOnly startDate = new DateOnly(2022, 1, 1);

            for (int i = 0; i < 10; i++)
            {
                string userName = userNames[random.Next(userNames.Length)];
                DateOnly entryDate = startDate.AddDays(random.Next(0, 5)); // Random date within a 5-day range

                if (!dailyEntries.ContainsKey((userName, entryDate)))
                {
                    dailyEntries[(userName, entryDate)] = new List<Timesheet>();
                }

                int totalWorkedHoursBefore = dailyEntries[(userName, entryDate)].Sum(entry => entry.HoursWorked);

                int maxHoursRemaining = 8 - totalWorkedHoursBefore;
                if (maxHoursRemaining <= 0) continue;

                int hoursWorked = random.Next(1, Math.Min(maxHoursRemaining, 4) + 1);

                var newEntry = new Timesheet
                {
                    UserName = userName,
                    Date = entryDate,
                    ProjectName = projectNames[i % projectNames.Length],
                    TaskDesc = taskDescs[i % taskDescs.Length],
                    HoursWorked = hoursWorked,
                    TotalHoursWorked = totalWorkedHoursBefore + hoursWorked
                };

                dailyEntries[(userName, entryDate)].Add(newEntry);

                foreach (var entry in dailyEntries[(userName, entryDate)])
                {
                    entry.TotalHoursWorked = totalWorkedHoursBefore + hoursWorked;
                }

                res.Add(newEntry);
            }

            return res;
        }
    }
}
