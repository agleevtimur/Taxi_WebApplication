using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Taxi_Database.Context;

namespace Taxi.Controllers
{
    [Authorize(Roles = "employee")]
    public class OrderController : Controller
    {
        private readonly ApplicationContext context;

        public OrderController(ApplicationContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            IOrderController repository = new Orders(context);
            var model = repository.Index();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IOrderController repository = new Orders(context);
            var model = repository.CreateGet();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel model, string id)
        {
            IOrderController repository = new Orders(context);
            if (ModelState.IsValid)
            {
                await repository.Create(model.LocationFrom, model.LocationTo, model.Time, model.CountOfPeople, id);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Order(int? id)
        {
            IOrderController repository = new Orders(context);

            if (id == null)
                return NotFound();

            var model = repository.GetOrder((int)id);
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult ReadyOrders(string id)
        {
            IOrderController repository = new Orders(context);
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
            IOrderController repository = new Orders(context);
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
            IOrderController repository = new Orders(context);

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
            IOrderController repository = new Orders(context);
            repository.DeleteOrder(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Rating(string whoId, string whomId, int orderId, int newRating)
        {
            IOrderController repository = new Orders(context);
            await repository.Rating(whoId, whomId, orderId, newRating);
            return RedirectToAction("Index");
        }
    }
}
