using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.ViewModels
{
    public class GetSubscriptionViewModel
    {
        [Required]
        [MaxLength(100)]
        public string NameOfCard { get; set; }
        [Required]
        [MaxLength(16)]
        public string CardNumber { get; set; }
        [Required]
        public DateTime ExpiryTime { get; set; }
        [Required]
        [MaxLength(3)]
        public string SecretCode { get; set; }
    }
}
