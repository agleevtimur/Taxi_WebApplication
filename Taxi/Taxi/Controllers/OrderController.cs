using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Repository;

namespace Taxi.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationContext context;
        private readonly LocationService locationService;

        public OrderController(ApplicationContext context, LocationService locationService)
        {
            this.context = context;
            this.locationService = locationService;
        }

        public IActionResult Index(string id)
        {
            IOrderController repository = new Orders(context, locationService);
            var model = repository.Index(id);
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            IOrderController repository = new Orders(context, locationService);
            repository.DeleteOrder(int.Parse(id));
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string id)
        {
            if (id == null)
                return NotFound();
            IOrderController repository = new Orders(context, locationService);
            var model = repository.CreateGet(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            IError error = new Error();
            if (model.Id == null)
                return NotFound();
            IOrderController repository = new Orders(context, locationService);
            if (ModelState.IsValid)
            {
                if (model.LocationFrom == model.LocationTo)
                {
                    var newModel = error.GetError("Ошибка", "Место отправления не может совпадать с местом назначения");
                    return View("Error", newModel);
                }
                if (model.CountOfPeople == 0)
                {
                    var newModel = error.GetError("Ошибка", "Не введено число людей, отправляющихся в путь");
                    return View("Error", newModel);
                }

                await repository.Create(model.LocationFrom, model.LocationTo, model.Time, model.CountOfPeople, model.Id);
                return RedirectToRoute(new
                {
                    controller = "Order",
                    action = "Index",
                    id = model.Id
                });
            }
            var errorModel = error.GetError("Ошибка", "Ошибка в данных, убедитесь, что все поля были заполнены");
            return View("Error", errorModel);
        }

        public IActionResult Order(int id)
        {
            IOrderController repository = new Orders(context, locationService);

            if (id == 0)
                return NotFound();

            var model = repository.GetOrder(id);
            return View(model);
        }

        public IActionResult Locations()
        {
            IOrderController repository = new Orders(context, locationService);
            var model = repository.Locations();
            return View(model);
        }

        public IActionResult ReadyOrders(string id)
        {
            IOrderController repository = new Orders(context, locationService);
            if (id == null)
                return NotFound();

            var model = repository.GetOrdersByClientId(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rating(int id, OrderWithClientViewModel model)
        {
            IError error = new Error();
            IOrderController repository = new Orders(context, locationService);
            if (await repository.Find(model.UserName, model.WhomName, id) == true)
            {
                var errorModel = error.GetError("Ошибка", "Нельзя дважды оценивать попутчика");
                return View("Error", errorModel);
            }
            if (model.Rate < 1 || model.Rate > 5)
            {
                var errorModel = error.GetError("Ошибка", "Значение оценки должно быть в промежутке от 1 до 5 включительно");
                return View("Error", errorModel);
            }
            await repository.Rating(model.UserName, model.WhomName, id, model.Rate);
            return RedirectToAction("Index");
        }
    }
}
