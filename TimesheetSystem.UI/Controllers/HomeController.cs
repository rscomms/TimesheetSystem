using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TimesheetSystem.UI.Models;
using TimesheetSystem.UI.Repositories;

namespace TimesheetSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITimesheetDataRepository _repository;

        public HomeController(ITimesheetDataRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var dataEntries = _repository.GetAll();
            return View(dataEntries);
        }

        [HttpPost]
        public IActionResult Add(Timesheet entry)
        {
            if (entry.HoursWorked == 0)
            {
                TempData["ErrorMessage"] = "Invalid no. of hours worked.";
                return RedirectToAction("Index");
            }

            string userName = entry.UserName?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(entry.UserName))
            {
                TempData["ErrorMessage"] = "User name is required.";
                return RedirectToAction("Index");
            }

            DateOnly entryDate = entry.Date;

            int remainingHours = Math.Max(0, (8 - _repository.getRemainingHours(userName, entryDate)));
            if (remainingHours == 0 || entry.HoursWorked > remainingHours)
            {
                TempData["ErrorMessage"] = $"Invalid hours worked. User {userName} can only work for {remainingHours} hour/s more.";
            }
            else
            {
                _repository.Add(entry);
                //update the total worked hours for every data
                _repository.updateWorkingHours(userName, entryDate);
                TempData["SuccessMessage"] = $"Entry added successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}
