using System.ComponentModel.DataAnnotations;

namespace Taxi.ViewModels.Location
{
    public class AddLocationViewModel
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        public string GoogleCode { get; set; }
        [Required]
        public string YandexCode { get; set; }
        [Required]
        public string TwoGisCode { get; set; }
    }
}
