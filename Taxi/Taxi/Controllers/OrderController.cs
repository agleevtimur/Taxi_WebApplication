﻿//using BusinessLogic;
//using BusinessLogic.ControllersForMVC;
//using BusinessLogic.ModelsForControllers;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Services.Aop;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Taxi_Database.Context;
//using Taxi_Database.Repository;

//namespace Taxi.Controllers
//{
//    [Authorize]
//    public class OrderController : Controller
//    {
//        private readonly ApplicationContext context;
//        private readonly LocationService locationService;
//        private readonly ILogger<OrderController> _logger;
//        public OrderController(ApplicationContext context, LocationService locationService, ILogger<OrderController> logger)
//        {
//            this.context = context;
//            this.locationService = locationService;
//            _logger = logger;
//        }

//        public IActionResult Index(string id)
//        {
//            var order = new Orders(context, locationService);
//            var model = order.Index(id);
//            return View(model);
//        }

//        [HttpDelete]
//        public IActionResult Delete(string id)
//        {
//            var order = new Orders(context, locationService);
//            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
//            repository.DeleteOrder(int.Parse(id));
//            return Ok();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(string id)
//        {
//            if (id == null)
//                return NotFound();
//            var order = new Orders(context, locationService);
//            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
//            var model = repository.CreateGet(id);
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
//        {
//            IError error = new Error();
//            if (model.Id == null)
//                return NotFound();
//            var order = new Orders(context, locationService);
//            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
//            if (ModelState.IsValid)
//            {
//                if (model.LocationFrom == model.LocationTo)
//                {
//                    var newModel = error.GetError("Ошибка", "Место отправления не может совпадать с местом назначения");
//                    return View("Error", newModel);
//                }
//                await repository.Create(model.LocationFrom, model.LocationTo, model.Time, model.CountOfPeople, model.Id);
//                return RedirectToRoute(new
//                {
//                    controller = "Order",
//                    action = "Index",
//                    id = model.Id
//                });
//            }
//            var errorModel = error.GetError("Ошибка", "Ошибка в данных, убедитесь, что все поля были заполнены");
//            return View("Error", errorModel);
//        }

//        public IActionResult Order(int? id)
//        {
//            var order = new Orders(context, locationService);
//            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();

//            if (id == null)
//                return NotFound();

//            var model = repository.GetOrder((int)id);
//            return View(model);
//        }

//        public IActionResult ReadyOrders(string id)
//        {
//            var order = new Orders(context, locationService);
//            IOrderController repository = new Factory<IOrderController, Orders>(_logger, order).Create();
//            if (id == null)
//                return NotFound();

//            var orders = repository.GetOrdersByClientId(id);

//            if (orders == null)
//                return NotFound();

//            return View(orders);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Rating(string whoId, string whomId, int orderId, int newRating)
//        {
//            IOrderController repository = new Orders(context, locationService);
//            await repository.Rating(whoId, whomId, orderId, newRating);
//            return RedirectToAction("Index");
//        }

//        public IActionResult DestinationIsDifferent([Bind(Prefix = "LocationFrom")] string from, [Bind(Prefix = "LocationTo")] string to)
//        {
//            if (from == to) return Json("Смените место назначения");
//            else return Json(true);
//        }
//        public IActionResult StartIsDifferent([Bind(Prefix = "LocationFrom")] string from, [Bind(Prefix = "LocationTo")] string to)
//        {
//            if (from == to) return Json("Смените место отправки");
//            else return Json(true);
//        }
//    }
//}
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
        public CardActuals(int id, int startPoint, int finishPoint, DateTime departureTime, DateTime orderTime, int priority, int userId)
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
        public int StartPoint { get; set; }
        public int FinishPoint { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int Priority { get; set; }
        public int UserId { get; set; }
    }

    public class CardReady
    {
        public CardReady(int id, int startPoint, int finishPoint, DateTime orderTime, List<Client> clients)
        {
            Id = id;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            OrderTime = orderTime.ToString();
            Clients = clients;
        }

        public int Id { get; set; }
        public int StartPoint { get; set; }
        public int FinishPoint { get; set; }
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

        public IActionResult Index(string id)
        {
            IOrderController repository = new Orders(context, locationService);
            var model = repository.GetModel(id);
            return View(model);
        }
        [HttpGet]
        public Result Get()
        {
            IOrderController repository = new Orders(context, locationService);
            IRepository repository1 = new Repository(context);
            var data = repository.Index();
            var ready = data.ReadyOrders
            .Select(x => new CardReady(x.Id, x.StartPointId, x.FinishPointId, x.OrderTime, repository1.GetPassengers(x.Id))).ToArray();
            var actuals = data.Orders.Select(x => new CardActuals(x.Id, x.StartPointId, x.StartPointId, x.DepartureTime, x.OrderTime, x.Priority, x.UserId)).ToArray();
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
            IOrderController repository = new Orders(context, locationService);

            if (id == null)
                return
                NotFound();

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

        //[Authorize(Roles = "admin")]
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        // IOrderController repository = new Orders(context, locationService);

        // if (id == null)
        // return NotFound();

        // var order = repository.GetReadyOrderId((int)id);
        // if (order == null)
        // return NotFound();
        // return View(order);
        //}

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int id)
        //{
        // IOrderController repository = new Orders(context, locationService);
        // repository.DeleteOrder(id);
        // return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Rating(string whoId, string whomId, int orderId, int newRating)
        //{
        // IOrderController repository = new Orders(context, locationService);
        // await repository.Rating(whoId, whomId, orderId, newRating);
        // return RedirectToAction("Index");
        //}
    }
}
