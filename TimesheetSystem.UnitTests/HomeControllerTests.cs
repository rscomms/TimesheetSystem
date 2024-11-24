using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using TimesheetSystem.UI.Controllers;
using TimesheetSystem.UI.Models;
using TimesheetSystem.UI.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace TimesheetSystem.UnitTests
{
    public class HomeControllerTests
    {
        private readonly Mock<ITimesheetDataRepository> _repositoryMock;
        private readonly HomeController _controller;
        private readonly Mock<ITempDataDictionary> _tempDataMock;

        public HomeControllerTests()
        {
            _repositoryMock = new Mock<ITimesheetDataRepository>();
            _tempDataMock = new Mock<ITempDataDictionary>();
            _controller = new HomeController(_repositoryMock.Object)
            {
                TempData = _tempDataMock.Object
            };
        }
    }
}
