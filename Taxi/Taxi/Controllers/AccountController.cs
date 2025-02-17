﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Taxi.ViewModels.Login;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace Taxi.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IPasswordValidator<User> _passwordValidator;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IEmailSender emailSender, IPasswordValidator<User> passwordValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _passwordValidator = passwordValidator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            IUser repository = new UserRepository(_userManager);

            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.UserName };
                // добавляем пользователя
                var result = await repository.CreateUser(user, model.Password);

                if (result.Succeeded)
                {
                    //установка куки
                    await _signInManager.SignInAsync(user, false);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");

                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }// ToDo переопределить UserManager
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            IUser repository = new UserRepository(_userManager);

            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await repository.FindUser(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await repository.ConfirmEmail(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            else
                return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternaLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
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
            IUser repository = new UserRepository(_userManager);

            var user = await repository.FindUserByEmail(email);
            return user == null ? Json(true) : Json($"Почта '{email}' уже занята");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> UserNameInUse(string userName)
        {
            IUser repository = new UserRepository(_userManager);

            if (userName == null) return Json("Имя пользователя не корректно");
            var user = await repository.FindUserByName(userName);

            return user == null ? Json(true) : Json($"Имя пользователя '{userName}' уже занято");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> PasswordIsStrong(string password)
        {
            var result = await _passwordValidator.ValidateAsync(null, new User(), password);
            if (result.Succeeded) return Json(true);
            else return Json(result.Errors.FirstOrDefault().Description);
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
            IUser repository = new UserRepository(_userManager);

            if (ModelState.IsValid)
            {
                var user = await repository.FindUserByEmail(model.Email);

                if (user == null || await repository.IsConfirmEmail(user))
                {
                    // пользователь с данным email может отсутствовать в бд
                    // тем не менее мы выводим стандартное сообщение, чтобы скрыть 
                    // наличие или отсутствие пользователя в бд
                    ViewData["Message"] = "Сбрасывать пароль могут лишь пользователи с подтвержденной почтой";
                    return View("ForgotPasswordConfirmation");
                }

                ViewData["Message"] = "Для сброса пароля перейдите по ссылке в письме, отправленном на ваш email.";
                var code = await repository.GeneratePassword(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                    $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");

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
            IUser repository = new UserRepository(_userManager);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await repository.FindUserByEmail(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }

            var result = await repository.ResetPassword(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            IUser repository = new UserRepository(_userManager);
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternaLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Ошибка внешнего провайдера {remoteError}");
                return View("Login", model);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Ошибка при загрузке внешней информации для входа");
                return View("Login", model);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user = await repository.FindUserByEmail(email);

                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        var result = await _userManager.CreateAsync(user);
                        result.Errors.ToList();
                    }
                    await repository.AddLogin(user, info);
                    await _signInManager.SignInAsync(user, false);

                    return LocalRedirect(returnUrl);
                }
                ViewBag.ErrorType = $"Email Claim не получен от {info.LoginProvider}";
                ViewBag.ErrorType = "Пожалуйста, обратитесь за помощью fantomas2213@gmail.com";

                return View("Error");
            }
        }
    }
}
