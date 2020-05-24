using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taxi.ViewModels.Location;
using Taxi_Database.Context;
using Taxi_Database.Repository;

namespace Taxi.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly LocationService locationService;
        private readonly ApplicationContext context;

        public LocationController(LocationService locationService, ApplicationContext context)
        {
            this.locationService = locationService;
            this.context = context;
        }

        public IActionResult Index()
        {
            ILocationController repository = new Locations(locationService, context);
            var model = repository.Index();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult History()
        {
            ILocationController repository = new Locations(locationService, context);
            var model = repository.History();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLocationViewModel model)
        {
            IError error = new Error();
            ILocationController repository = new Locations(locationService, context);
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
    }
}
