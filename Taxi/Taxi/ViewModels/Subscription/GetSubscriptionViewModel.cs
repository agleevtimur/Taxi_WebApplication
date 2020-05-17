using System.ComponentModel.DataAnnotations;

namespace Taxi.ViewModels.Subscription
{
    public class GetSubscriptionViewModel
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public int CountOfTravels { get; set; }

        [Required]
        [MaxLength(19)]
        public string NameOfCard { get; set; }
        [Required]
        [MaxLength(5)]
        public string ExpiryTime { get; set; }

        [Required]
        [MaxLength(3)]
        public string SecretCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
