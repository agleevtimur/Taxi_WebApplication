﻿using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using DnsClient.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Aop;
using Taxi.ViewModels.Subscription;
using Taxi.ViewModels.User;
using Taxi_Database.Context;
using Taxi_Database.Models;

namespace Taxi.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserManager<User> userManager, ApplicationContext context, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            this.context = context;
            _logger = logger;
        }
        [Authorize]
        public async Task<IActionResult> Index(string id)
        {
            if (User.IsInRole("admin")) return RedirectToAction(actionName: "Admin", controllerName: "Users");
            if (User.IsInRole("employee")) return RedirectToAction(actionName: "Index", controllerName: "Home");
            var user = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, user).Create();

            var client = await repository.Index(id);
            if (client == null)
                return NotFound();
            var model = repository.Information(client);
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "admin, employee")]
        public IActionResult Clients()
        {
            var user = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, user).Create();

            var model = repository.Clients();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View();

        

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var userProxy = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, userProxy).Create();

            User user = await repository.FindUser(id);
            if (user == null)
                 return NotFound();

            var model = await repository.EditGet(user);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var userProxy = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, userProxy).Create();

            if (ModelState.IsValid)
            {
                User user = await repository.FindUser(model.Id);
                if (user != null)
                {
                    var result = await repository.EditPost(user, model);
                    if (result.Succeeded)
                        return RedirectToRoute(new
                        {
                            controller = "Users",
                            action = "Index",
                            id = model.Id
                        });

                    else
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, user).Create();
            var result = await repository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var userProxy = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, userProxy).Create();

            User user = await repository.FindUser(id);
            if (user == null)
                return NotFound();
            var model = repository.ChangeGet(user.Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var userProxy = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, userProxy).Create();

            if (ModelState.IsValid)
            {
                User user = await repository.FindUser(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await repository.ChangePost(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                        return RedirectToRoute(new
                        {
                            controller = "Users",
                            action = "Index",
                            id = model.Id
                        });

                    else
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Subscription()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Subscription(GetSubscriptionViewModel model)
        {
            var userProxy = new Users(_userManager, context);
            IUserController repository = new Factory<IUserController, Users>(_logger, userProxy).Create();

            repository.Subscription(model.Priority, model.CountOfTravels, model.Id);
            return RedirectToAction("Index");
        }
        
    }
}
