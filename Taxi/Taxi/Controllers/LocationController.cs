using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using DnsClient.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Aop;
using System.Threading.Tasks;
using Taxi.ViewModels.Location;
using Taxi_Database.Context;
using Taxi_Database.Repository;

namespace Taxi.Controllers
{
    [Authorize(Roles = "admin, employee")]
    public class LocationController : Controller
    {
        private readonly LocationService locationService;
        private readonly ApplicationContext context;
        private readonly ILogger<LocationController> _logger;

        public LocationController(LocationService locationService, ApplicationContext context, ILogger<LocationController> logger)
        {
            this.locationService = locationService;
            this.context = context;
            _logger = logger;
        }
        [Authorize(Roles = "admin, employee")]
        public IActionResult Index()
        {
            var location = new Locations(locationService, context);
            ILocationController repository = new Factory<ILocationController, Locations>(_logger, location).Create();
            var model = repository.Index();
            return View(model);
        }

        [Authorize(Roles = "admin, employee")]
        public IActionResult History()
        {
            var location = new Locations(locationService, context);
            ILocationController repository = new Factory<ILocationController, Locations>(_logger, location).Create();
            var model = repository.History();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddLocationViewModel model)
        {
            IError error = new Error();
            var location = new Locations(locationService, context);
            ILocationController repository = new Factory<ILocationController, Locations>(_logger, location).Create();
            if (ModelState.IsValid)
            {
                if(await repository.IsInLocations(model.Name) == true)
                {
                    var newModel = error.GetError("Ошибка", "Такая локация в базе данных уже есть");
                    return View("Error", newModel);
                }
                await repository.SavePost(model.Name, model.GoogleCode, model.YandexCode, model.TwoGisCode);
                return RedirectToAction("Index");
            }
            var newmodel = error.GetError("Ошибка", "Ошибка в данных, убедитесь, что все поля были заполнены");
            return View("Error", newmodel);
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> LocationInUse([Bind(Prefix = "Name")]string location)
        {
            var locationProxy = new Locations(locationService, context);
            ILocationController repository = new Factory<ILocationController, Locations>(_logger, locationProxy).Create();
            if (await repository.IsInLocations(location)) return Json("Данная локация уже используется");
            else return Json(true);
        }
    }
}
