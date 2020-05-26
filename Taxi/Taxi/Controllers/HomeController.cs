using System.Diagnostics;
using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Aop;
using Taxi_Database.Context;
using Taxi_Database.Models;

namespace Taxi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            var home = new Home(context);
            IHomeController repository = new Factory<IHomeController, Home>(_logger, home).Create();
            var model = repository.Index();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel("Ошибка", "Ошибка в выгрузке данных"));
        }
    }
}
