using System.ComponentModel.DataAnnotations;

namespace Taxi.ViewModels.Login
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
