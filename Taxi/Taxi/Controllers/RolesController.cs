using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Models;
using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.Extensions.Logging;
using Services.Aop;

namespace Taxi.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var role = new Role(_roleManager, _userManager);
            IRoleController repository = new Factory<IRoleController, Role>(_logger, role).Create();
            var model = repository.Index();
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            var role = new Role(_roleManager, _userManager);
            IRoleController repository = new Factory<IRoleController, Role>(_logger, role).Create();

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
            var role = new Role(_roleManager, _userManager);
            IRoleController repository = new Factory<IRoleController, Role>(_logger, role).Create();

            var result = await repository.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult UserList()
        {
            var role = new Role(_roleManager, _userManager);
            IRoleController repository = new Factory<IRoleController, Role>(_logger, role).Create();

            var model = repository.UserList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var role = new Role(_roleManager, _userManager);
            IRoleController repository = new Factory<IRoleController, Role>(_logger, role).Create();

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
            var role = new Role(_roleManager, _userManager);
            IRoleController repository = new Factory<IRoleController, Role>(_logger, role).Create();
            
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
