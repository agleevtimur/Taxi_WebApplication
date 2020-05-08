using System.ComponentModel.DataAnnotations;

namespace Taxi.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
