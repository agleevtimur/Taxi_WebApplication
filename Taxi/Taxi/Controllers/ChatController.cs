using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;

namespace Taxi.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<User> _userManager;
        public ChatController(IdentityContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IChatController repository = new Chat(_context, _userManager);
            var currentUser = await repository.GetUser(User);
            ViewBag.CurrentUserName = currentUser.UserName;
            var messages = await repository.GetMessages();

            return View(messages);
        }
        public async Task<IActionResult> Create(Message message)
        {
            IChatController repository = new Chat(_context, _userManager);
            if (ModelState.IsValid)
            {
                await repository.Messages(User, message);
                return Ok();
            }
            return View("Error");
        }
    }
}
