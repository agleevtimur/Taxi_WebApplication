using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace Taxi.Controllers
{
    public class CardActuals
    {
        public CardActuals(int id, string startPoint, string finishPoint, DateTime departureTime, DateTime orderTime, int priority, int userId)
        {
            Id = id;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            DepartureTime = departureTime;
            OrderTime = orderTime;
            Priority = priority;
            UserId = userId;
        }

        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int Priority { get; set; }
        public int UserId { get; set; }
    }

    public class CardReady
    {
        public CardReady(int id, string startPoint, string finishPoint, DateTime orderTime, List<Client> clients)
        {
            Id = id;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            OrderTime = orderTime.ToString();
            Clients = clients;
        }

        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public string OrderTime { get; set; }
        public List<Client> Clients { get; set; }
    }

    public class Result
    {
        public Result(CardReady[] readys, CardActuals[] actuals)
        {
            Readys = readys;
            Actuals = actuals;
        }

        public CardReady[] Readys { get; set; }
        public CardActuals[] Actuals { get; set; }
    }

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

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public Result Get()
        {
            IOrderController repository = new Orders(context, locationService);
            IRepository repository1 = new Repository(context);
            var data = repository.Index();
            var ready = data.ReadyOrders
            .Select(x => new CardReady(x.Id, x.StartPointId.ToString(), x.FinishPointId.ToString(), x.OrderTime, new List<Client>())).ToArray();
            var actuals = data.Orders.Select(x => new CardActuals(x.Id, x.StartPointId.ToString(), x.StartPointId.ToString(), x.DepartureTime, x.OrderTime, x.Priority, x.UserId)).ToArray();
            var result = new Result(ready, actuals);
            return result;
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

        public IActionResult DestinationIsDifferent([Bind(Prefix = "LocationFrom")] string from, [Bind(Prefix = "LocationTo")] string to)
        {
            if (from == to) return Json("Смените место назначения");
            else return Json(true);
        }
        public IActionResult StartIsDifferent([Bind(Prefix = "LocationFrom")] string from, [Bind(Prefix = "LocationTo")] string to)
        {
            if (from == to) return Json("Смените место отправки");
            else return Json(true);
        }

        //[Authorize(Roles = "admin")]
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    IOrderController repository = new Orders(context, locationService);

        //    if (id == null)
        //        return NotFound();

        //    var order = repository.GetReadyOrderId((int)id);
        //    if (order == null)
        //        return NotFound();
        //    return View(order);
        //}

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        //    IOrderController repository = new Orders(context, locationService);
        //    repository.DeleteOrder(id);
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Rating(string whoId, string whomId, int orderId, int newRating)
        //{
        //    IOrderController repository = new Orders(context, locationService);
        //    await repository.Rating(whoId, whomId, orderId, newRating);
        //    return RedirectToAction("Index");
        //}
    }
}
