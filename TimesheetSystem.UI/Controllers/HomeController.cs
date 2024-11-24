using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TimesheetSystem.UI.Models;

namespace TimesheetSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
