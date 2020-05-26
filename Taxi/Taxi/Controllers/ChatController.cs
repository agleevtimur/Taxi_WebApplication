using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using DnsClient.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Aop;
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
        private readonly ILogger<ChatController> _logger;
        public ChatController(IdentityContext context, UserManager<User> userManager, ILogger<ChatController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var chat = new Chat(_context, _userManager);
            IChatController repository = new Factory<IChatController, Chat>(_logger,chat).Create();
            var currentUser = await repository.GetUser(User);
            ViewBag.CurrentUserName = currentUser.UserName;
            var messages = await repository.GetMessages();

            return View(messages);
        }
        public async Task<IActionResult> Create(Message message)
        {
            IError error = new Error();
            var chat = new Chat(_context, _userManager);
            IChatController repository = new Factory<IChatController, Chat>(_logger, chat).Create();
            if (ModelState.IsValid)
            {
                await repository.Messages(User, message);
                return Ok();
            }
            var model = error.GetError("Ошибка", "Не удалось отправить сообщение, возникла ошибка в базе данных");
            return View("Error", model);
        }
    }
}
