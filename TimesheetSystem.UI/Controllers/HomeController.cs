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
    }
}
