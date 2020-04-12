using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Login
    {
        [Required]
        [MaxLength(50)]
        public string ClientName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
