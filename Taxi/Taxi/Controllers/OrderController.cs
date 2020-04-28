using BusinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace Taxi.Controllers
{
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
            IRepository repository = new Repository(context);
            var orders = repository.GetRequests();
            return View(orders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            IRepository repository = new Repository(context);
            if (ModelState.IsValid)
            {
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Index));
            }

            return View(order);
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
