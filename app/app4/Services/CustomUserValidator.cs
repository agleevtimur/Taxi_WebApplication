using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace app4.Models
{
    public class CustomUserValidator : IUserValidator<User>
    {
        private string email_pattern { get; } = @"^\w+@[a-z]+\.[a-z]+(\.[a-z]+)?$";
        private string login_pattern { get; } = @"^[a-z]+\w+$";
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            LoginValidation(user.UserName, errors);
            EmailValidation(user.Email, errors);

            if (errors.Count == 0) CheckUniq(manager, user, errors);
                
            
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }

        private void LoginValidation(string userName, List<IdentityError> errors)
        {
            if (!Regex.IsMatch(userName, @"\W"))
            {
                if (!Regex.IsMatch(userName, @"^[0-9]"))
                {
                    if (!Regex.IsMatch(userName, @"(.)\1{4,}"))
                    {
                        if (!Regex.IsMatch(userName, login_pattern))
                            errors.Add(new IdentityError { Description = "Не корректное имя пользователя" });
                    }
                    else errors.Add(new IdentityError { Description = "Имя пользователя не должно включать повторяющиеся подряд символы" });
                }
                else errors.Add(new IdentityError { Description = "Имя пользователя не должно начинаться с цифры" });
            }
            else errors.Add(new IdentityError { Description = "Имя пользователя не должно включать специальные символы" });
        }
        private void EmailValidation(string email, List<IdentityError> errors)
        {
            if (email.Contains('@'))
            {
                if (!email.ToLower().EndsWith("@spam.com"))
                {
                    if (!Regex.IsMatch(email.ToLower(), email_pattern))
                    {
                        errors.Add(new IdentityError { Description = "Некорректный email" });
                    }
                }
                else errors.Add(new IdentityError { Description = "Данный домен находится в спам - базе. Выберите другой почтовый сервис" });

            }
            else errors.Add(new IdentityError { Description = "Email должен содержать \'@\'" });
        }
        private async void CheckUniq(UserManager<User> manager, User user, List<IdentityError> errors)
        {
            foreach (var otherUser in manager.Users)
            {
                if (otherUser.Email == user.Email)
                {
                    errors.Add(new IdentityError { Description = $"Email {user.Email} уже существует" });
                    break;
                }
                if (otherUser.UserName == user.UserName)
                {
                    errors.Add(new IdentityError { Description = $"Имя пользователя {user.UserName} уже существует" });
                    break;
                }
            }
        }
    }

    public static class UserManagerExtension
    {
        public static async Task<IQueryable<User>> GetUsersAsync(this UserManager<User> userManager)
        {
            return await Task.Run(() =>
            {
                return userManager.Users;
            });
        }
    }
}
