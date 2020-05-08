using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taxi.ViewModels.User;
using Taxi_Database.Models;

namespace Taxi.Controllers
{
    [Authorize(Roles = "employee")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IUserController repository = new Users(_userManager);

            var model = repository.Index();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            IUserController repository = new Users(_userManager);

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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IUserController repository = new Users(_userManager);

            User user = await repository.FindUser(id);
            if (user == null)
                 return NotFound();

            var model = repository.EditGet(user);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            IUserController repository = new Users(_userManager);

            if (ModelState.IsValid)
            {
                User user = await repository.FindUser(model.Id);
                if (user != null)
                {
                    var result = await repository.EditPost(user, model);
                    if (result.Succeeded)
                        return RedirectToAction("Index");

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
            IUserController repository = new Users(_userManager);
            var result = await repository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            IUserController repository = new Users(_userManager);

            User user = await repository.FindUser(id);
            if (user == null)
                return NotFound();
            var model = repository.ChangeGet(user.Id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            IUserController repository = new Users(_userManager);

            if (ModelState.IsValid)
            {
                User user = await repository.FindUser(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await repository.ChangePost(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                        return RedirectToAction("Index");

                    else
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                }
                else
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return View(model);
        }
    }
}
