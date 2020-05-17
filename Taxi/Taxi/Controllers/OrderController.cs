using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Taxi.ViewModels.Order;
using Taxi_Database.Context;
using Taxi_Database.Repository;

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
        // в процессе
        public IActionResult Index()
        {
            IOrderController repository = new Orders(context);
            var orders = repository.Index();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateOrderViewModel model)
        {
            IOrderController repository = new Orders(context);
            if (ModelState.IsValid)
            {
                //repository.SaveOrder(order);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Get(int? id)
        {
            IRepository repository = new Repository(context);
            if (id == null)
                return NotFound();

            var newId = Int32.Parse(id.ToString());
            var orders = repository.GetRequestsByClientId(newId);

            if (orders == null)
                return NotFound();

            return View(orders);
        }

        [Authorize(Roles = "admin")]
        public IActionResult History()
        {
            IRepository repository = new Repository(context);
            var orders = repository.GetOrders();
            return View(orders);
        }

        [HttpPost]
        [Authorize(Roles = "user, admin")]
        public IActionResult Rating(int whoId, int whomId, int orderId, int newRating)
        {
            IRating rate = new RatingContext();
            rate.Create(whoId, whomId, orderId, newRating);

            IRepository repository = new Repository(context);
            var countOfRates = repository.GetCountOfRates(whomId);
            var rating = repository.GetRating(whomId);

            INewRating newRate = new NewRating();
            var newCountOfRates = newRate.GetNewCountOfRates(countOfRates);
            var ratingOfClient = newRate.GetNewRating(countOfRates, rating, newRating);

            repository.UpdateRating(whomId, newCountOfRates, ratingOfClient);

            return RedirectToAction(nameof(Index));
        }
    }
}
