using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app4.ViewModels
{
    public class RegisterViewModel
    {
        [Remote(action: "EmailInUse", controller: "Account")]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Remote(action: "UserNameInUse", controller: "Account")]
        [Required(ErrorMessage = "Не указано имя пользователя")]
        [MinLength(5, ErrorMessage = "Слишком короткое имя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Remote(action: "PasswordIsStrong", controller: "Account")]
        [Required(ErrorMessage = "Не указан пароль")]
        [MinLength(5, ErrorMessage = "Слишком короткий пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }
    }
}
