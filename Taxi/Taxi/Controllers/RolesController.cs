using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Models;
using BusinessLogic;
using BusinessLogic.ControllersForMVC;

namespace Taxi.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            IRoleController repository = new Role(_roleManager, _userManager);
            var model = repository.Index();
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IRoleController repository = new Role(_roleManager, _userManager);

            if (!string.IsNullOrEmpty(name))
            {
                var result = await repository.Create(name);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IRoleController repository = new Role(_roleManager, _userManager);

            var result = await repository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult UserList()
        {
            IRoleController repository = new Role(_roleManager, _userManager);

            var model = repository.UserList();
            return View(model);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            IRoleController repository = new Role(_roleManager, _userManager);

            // получаем пользователя
            User user = await repository.FindUser(userId);
            if (user != null)
            {
                var model = await repository.EditGet(user);
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            IRoleController repository = new Role(_roleManager, _userManager);

            // получаем пользователя
            User user = await repository.FindUser(userId);
            if (user != null)
            {
                await repository.EditPost(user, roles);
                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
