using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            this.context = context;
        }
        [Authorize]
        public async Task<IActionResult> Index(string id)
        {
            IUserController repository = new Users(_userManager, context);

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

        public IActionResult Clients()
        {
            IUserController repository = new Users(_userManager, context);

            var model = repository.Clients();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            IUserController repository = new Users(_userManager, context);

            if (ModelState.IsValid)
            {
                var result = await repository.Create(model.Email, model.Login, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IUserController repository = new Users(_userManager, context);

            User user = await repository.FindUser(id);
            if (user == null)
                 return NotFound();

            var model = await repository.EditGet(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            IUserController repository = new Users(_userManager, context);

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
        public async Task<IActionResult> Delete(string id)
        {
            IUserController repository = new Users(_userManager, context);
            var result = await repository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            IUserController repository = new Users(_userManager, context);

            User user = await repository.FindUser(id);
            if (user == null)
                return NotFound();
            var model = repository.ChangeGet(user.Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            IUserController repository = new Users(_userManager, context);

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
            IUserController repository = new Users(_userManager, context);

            repository.Subscription(model.Priority, model.CountOfTravels, model.Id);
            return RedirectToAction("Index");
        }
    }
}
