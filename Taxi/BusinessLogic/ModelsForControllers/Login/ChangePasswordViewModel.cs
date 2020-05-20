using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ModelsForControllers
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        [Remote(action: "PasswordIsStrong", controller: "Account")]
        [Required(ErrorMessage = "Не указан пароль")]
        [MinLength(5, ErrorMessage = "Слишком короткий пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
