using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taxi.ViewModels.Location;
using Taxi_Database.Context;

namespace Taxi.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationContext context;
        public LocationController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ILocationController repository = new Locations(context);
            var model = repository.Index();
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult History()
        {
            ILocationController repository = new Locations(context);
            var model = repository.History();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddLocationViewModel model)
        {
            ILocationController repository = new Locations(context);
            if (ModelState.IsValid)
            {
                await repository.SavePost(model.Name, model.GoogleCode, model.YandexCode, model.TwoGisCode);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
