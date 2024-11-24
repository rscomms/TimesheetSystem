using Microsoft.EntityFrameworkCore;
using TimesheetSystem.UI.Models;
using TimesheetSystem.UI.Util;

public class ApplicationDbContext : DbContext
{
    public DbSet<Timesheet> Timesheets { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var mockEntries = GenerateMockData.MockTimesheetEntryModels();
        int nextId = 1; // Start after hardcoded IDs

        foreach (var entry in mockEntries)
        {
            entry.ID = nextId++; // Assign unique IDs
            modelBuilder.Entity<Timesheet>().HasData(entry);
        }
    }
}




