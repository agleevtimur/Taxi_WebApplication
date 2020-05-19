using BusinessLogic;
using BusinessLogic.ControllersForMVC;
using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Taxi.ViewModels.Login;
using Taxi_Database.Context;
using Taxi_Database.Models;

namespace Taxi.Controllers
{
    [Authorize(Roles = "employee")]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly ApplicationContext context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IEmailSender emailSender, IPasswordValidator<User> passwordValidator, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _passwordValidator = passwordValidator;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            IAccountController repository = new Account(_userManager, 
                _signInManager, _emailSender, _passwordValidator, context);

            if (ModelState.IsValid)
            {
                var user = repository.RegisterGet(model.Email, model.UserName);
                var result = await repository.Create(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await repository.Register(user, model.Password);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    return Content("Для завершения регистрации проверьте электронную почту " +
                        "и перейдите по ссылке, указанной в письме");

                }

                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                // ToDo переопределить UserManager
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);

            if (userId == null || code == null)
                return View("Error");

            var user = await repository.FindUser(userId);
            if (user == null)
                return View("Error");

            var result = await repository.ConfirmGet(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Users");
            else
                return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);
            var model = await repository.LoginGet(returnUrl);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);

            if (ModelState.IsValid)
            {
                var result = await repository.LoginPost(model);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                    {
                        var user = repository.GetUserByLogin(model.Login);
                        return RedirectToAction("Index/" + user.StringId, "Users");
                    }
                }
                else
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);
            await repository.LogOut();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> EmailInUse(string email)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);
            var user = await repository.EmailInUse(email);
            return user == null ? Json(true) : Json($"Почта '{email}' уже занята");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> UserNameInUse(string userName)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);

            if (userName == null) 
                return Json("Имя пользователя не корректно");
            var user = await repository.UserNameInUse(userName);

            return user == null ? Json(true) : Json($"Имя пользователя '{userName}' уже занято");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> PasswordIsStrong(string password)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);
            var result = await repository.PasswordIsStrong(password);
            if (result.Succeeded) 
                return Json(true);
            else 
                return Json(result.Errors.FirstOrDefault().Description);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);

            if (ModelState.IsValid)
            {
                var user = await repository.EmailInUse(model.Email);
                if (user == null || await repository.IsConfirmEmail(user))
                {
                    // пользователь с данным email может отсутствовать в бд
                    // тем не менее мы выводим стандартное сообщение, чтобы скрыть 
                    // наличие или отсутствие пользователя в бд
                    ViewData["Message"] = "Сбрасывать пароль могут лишь пользователи с подтвержденной почтой";
                    return View("ForgotPasswordConfirmation");
                }

                ViewData["Message"] = "Для сброса пароля перейдите по ссылке в письме, отправленном на ваш email.";
                var code = await repository.Generate(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, 
                    protocol: HttpContext.Request.Scheme);
                await repository.SendEmail(model.Email, callbackUrl);

                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);

            if (!ModelState.IsValid)
                return View(model);

            var user = await repository.EmailInUse(model.Email);
            if (user == null)
                return View("ResetPasswordConfirmation");

            var result = await repository.ResetPassword(user, model.Code, model.Password);
            if (result.Succeeded)
                return View("ResetPasswordConfirmation");

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            return repository.ExternalLogin(provider, redirectUrl);            
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            IAccountController repository = new Account(_userManager,
                _signInManager, _emailSender, _passwordValidator, context);
            returnUrl = returnUrl ?? Url.Content("~/");

            var model = await repository.LoginGet(returnUrl);
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Ошибка внешнего провайдера {remoteError}");
                return View("Login", model);
            }

            var info = await repository.GetInfo();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Ошибка при загрузке внешней информации для входа");
                return View("Login", model);
            }

            var signInResult = await repository.GetSignInResult(info);
            if (signInResult.Succeeded)
                return LocalRedirect(returnUrl);
            else
            {
                var email = repository.GetEmail(info);
                if (email != null)
                {
                    var user = await repository.EmailInUse(email);
                    if (user == null)
                    {
                        var result = await repository.LoginUser(info);
                        result.Errors.ToList();
                    }
                    await repository.ExternalLoginCallback(user, info);
                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorType = $"Email Claim не получен от {info.LoginProvider}";
                ViewBag.ErrorType = "Пожалуйста, обратитесь за помощью fantomas2213@gmail.com";

                return View("Error");
            }
        }
    }
}
