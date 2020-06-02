using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ModelsForControllers
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string AboutSelf { get; set; }
    }
}
