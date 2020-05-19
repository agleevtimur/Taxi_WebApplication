using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using Taxi_Database.Models;
using Taxi_Database.Repository;
using BusinessLogic.ModelsForControllers;
using System.Linq;
using System.Security.Claims;
using Taxi_Database.Context;
using System;

namespace BusinessLogic.ControllersForMVC
{
    public class Account : IAccountController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IPasswordValidator<User> passwordValidator;
        private readonly ApplicationContext context;

        public Account(UserManager<User> userManager, SignInManager<User> signInManager,
            IEmailSender emailSender, IPasswordValidator<User> passwordValidator,
            ApplicationContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.passwordValidator = passwordValidator;
            this.context = context;
        }

        public User RegisterGet(string email, string userName)
        {
            return new User { Email = email, UserName = userName };
        }

        public async Task<IdentityResult> Create(User user, string password)
        {
            IUser repository = new UserRepository(userManager);
            // добавляем пользователя
            var result = await repository.CreateUser(user, password);
            return result;
        }

        public Client GetUserByLogin(string login)
        {
            IRepository repository = new Repository(context);
            var user = repository.GetClientByLogin(login);
            return user;
        }

        public async Task<string> Register(User user, string password)
        {
            //установка куки
            await signInManager.SignInAsync(user, false);
            IRepository repository = new Repository(context);
            await repository.SaveClient(new Client(user.Id, "Требуется ввести", "Требуется ввести", user.UserName, user.Email, password,
               0, 0, 0, -1, null, 0, DateTime.Now));
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<User> FindUser(string userId)
        {
            IUser repository = new UserRepository(userManager);
            return await repository.FindUser(userId);
        }

        public async Task<IdentityResult> ConfirmGet(User user, string code)
        {
            IUser repository = new UserRepository(userManager);
            return await repository.ConfirmEmail(user, code);
        }

        public async Task<LoginViewModel> LoginGet(string returnUrl = null)
        {
            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternaLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginPost(LoginViewModel model)
        {
            await signInManager.SignOutAsync();
            var result = await signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
            return result;
        }

        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<User> EmailInUse(string email)
        {
            IUser repository = new UserRepository(userManager);
            var user = await repository.FindUserByEmail(email);
            return user;
        }

        public async Task<User> UserNameInUse(string userName)
        {
            IUser repository = new UserRepository(userManager);
            var user = await repository.FindUserByName(userName);
            return user;
        }

        public async Task<IdentityResult> PasswordIsStrong(string password)
        {
            return await passwordValidator.ValidateAsync(null, new User(), password);
        }

        public async Task<bool> IsConfirmEmail(User user)
        {
            IUser repository = new UserRepository(userManager);
            return await repository.IsConfirmEmail(user);
        }

        public async Task<string> Generate(User user)
        {
            IUser repository = new UserRepository(userManager);
            return await repository.GeneratePassword(user);
        }

        public async Task SendEmail(string email, string callbackUrl)
        {
            await emailSender.SendEmailAsync(email, "Reset Password",
                   $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
        }

        public async Task<IdentityResult> ResetPassword(User user, string code, string password)
        {
            IUser repository = new UserRepository(userManager);
            return await repository.ResetPassword(user, code, password);
        }

        public ChallengeResult ExternalLogin(string provider, string redirectUrl)
        {
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<ExternalLoginInfo> GetInfo()
        {
            return await signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> GetSignInResult(ExternalLoginInfo info)
        {
            return await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        }

        public string GetEmail(ExternalLoginInfo info)
        {
            return info.Principal.FindFirstValue(ClaimTypes.Email);
        }

        public async Task<IdentityResult> LoginUser(ExternalLoginInfo info)
        {
            IUser repository = new UserRepository(userManager);
            var user = new User
            {
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)
            };

            return await repository.CreateUser(user, null);
        }

        public async Task ExternalLoginCallback(User user, ExternalLoginInfo info)
        {
            IUser repository = new UserRepository(userManager);
            await repository.AddLogin(user, info);
            await signInManager.SignInAsync(user, false);
        }
    }
}
