using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Aop;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ILogger<OrderController> _logger;
        private static List<CardOrder> actuals;
        public OrderController(ApplicationContext context, LocationService locationService, ILogger<OrderController> logger)
        {
            this.context = context;
            this.locationService = locationService;
            _logger = logger;
            var order = new Orders(context, locationService);
            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
            actuals = repository.Index().Orders.Select(x => new CardOrder(x.Id, "start", "finish", x.DepartureTime, x.OrderTime, x.Priority, x.UserId)).ToList();
        }

        public IActionResult Index(string id)
        {
            var order = new Orders(context, locationService);
            var model = order.GetModel(id);
            return View(model);
        }
        [HttpGet]
        public List<CardOrder> GetActuals()
        {
            return actuals;
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var order = new Orders(context, locationService);
            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
            repository.DeleteOrder(int.Parse(id));
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string id)
        {
            if (id == null)
                return NotFound();
            var order = new Orders(context, locationService);
            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
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
            var order = new Orders(context, locationService);
            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
            if (ModelState.IsValid)
            {
                if (model.LocationFrom == model.LocationTo)
                {
                    var newModel = error.GetError("Ошибка", "Место отправления не может совпадать с местом назначения");
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

        public IActionResult Order(int? id)
        {
            var order = new Orders(context, locationService);
            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();

            if (id == null)
                return NotFound();

            var model = repository.GetOrder((int)id);
            return View(model);
        }

        public IActionResult ReadyOrders(string id)
        {
            var order = new Orders(context, locationService);
            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
            if (id == null)
                return NotFound();

            var orders = repository.GetOrdersByClientId(id);

            if (orders == null)
                return NotFound();

            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Rating(string whoId, string whomId, int orderId, int newRating)
        {
            IOrderController repository = new Orders(context, locationService);
            await repository.Rating(whoId, whomId, orderId, newRating);
            return RedirectToAction("Index");
        }

        public IActionResult DestinationIsDifferent([Bind(Prefix = "LocationFrom")] string from, [Bind(Prefix = "LocationTo")] string to)
        {
            if (from == to) return Json("Смените место назначения");
            else return Json(true);
        }
        public IActionResult StartIsDifferent([Bind(Prefix ="LocationFrom")] string from, [Bind(Prefix = "LocationTo")] string to)
        {
            if (from == to) return Json("Смените место отправки");
            else return Json(true);
        }
    }
}
