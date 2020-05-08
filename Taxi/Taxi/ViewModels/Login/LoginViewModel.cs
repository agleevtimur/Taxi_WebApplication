using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Taxi.ViewModels.Login
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternaLogins { get; set; }
    }
}
