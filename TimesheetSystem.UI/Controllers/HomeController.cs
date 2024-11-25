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
            //TO DO: Can be refactored to move the below logic and also the repo calls to a seperate service method,
            //as it is really minimal, I have left it here. 
            string userName = entry.UserName.Trim();
            DateOnly entryDate = entry.Date;

            int remainingHours = Math.Max(0, (8 - _repository.GetTotalHoursWorked(userName, entryDate)));
            if (remainingHours == 0 || entry.HoursWorked > remainingHours)
            {
                TempData["ErrorMessage"] = $"Exceeded daily hours limit. User {userName} can only work for {remainingHours} hour/s more.";
            }
            else
            {
                _repository.Add(entry);
                //update the total worked hours for every data
                _repository.UpdateWorkingHours(userName, entryDate);
                TempData["SuccessMessage"] = $"Entry added successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}
