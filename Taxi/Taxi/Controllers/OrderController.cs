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

        [Authorize]
        public IActionResult Index()
        {
            IOrderController repository = new Orders(context, locationService);
            var model = repository.Index();
            return View(model);
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
                await repository.Create(model.LocationFrom, model.LocationTo, model.Time, model.CountOfPeople, model.Id);
                return RedirectToAction("Index", "Order");
            }
            var errorModel = error.GetError("Ошибка", "Ошибка в данных, убедитесь, что все поля были заполнены");
            return View("Error", errorModel);
        }
         
        public IActionResult Order(int? id)
        {
            IOrderController repository = new Orders(context, locationService);

            if (id == null)
                return NotFound();

            var model = repository.GetOrder((int)id);
            return View(model);
        }

        public IActionResult ReadyOrders(string id)
        {
            IOrderController repository = new Orders(context, locationService);
            if (id == null)
                return NotFound();

            var orders = repository.GetOrdersByClientId(id);

            if (orders == null)
                return NotFound();

            return View(orders);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Requests(string id)
        {
            IOrderController repository = new Orders(context, locationService);
            if (id == null)
                return NotFound();

            var orders = repository.GetRequestsByClientId(id);

            if (orders == null)
                return NotFound();

            return View(orders);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            IOrderController repository = new Orders(context, locationService);

            if (id == null)
                return NotFound();

            var order = repository.GetReadyOrderId((int)id);
            if (order == null)
                return NotFound();
            return View(order);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            IOrderController repository = new Orders(context, locationService);
            repository.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Rating(string whoId, string whomId, int orderId, int newRating)
        {
            IOrderController repository = new Orders(context, locationService);
            await repository.Rating(whoId, whomId, orderId, newRating);
            return RedirectToAction("Index");
        }
    }
}
