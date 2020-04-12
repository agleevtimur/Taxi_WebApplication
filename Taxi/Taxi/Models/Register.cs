using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Register
    {
        [Required]
        [MaxLength(50)]
        public string SecondName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(60)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClientName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
