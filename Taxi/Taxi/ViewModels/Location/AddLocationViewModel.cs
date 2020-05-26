using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Taxi.ViewModels.Location
{
    public class AddLocationViewModel
    {
        [Required(ErrorMessage = "Введите название локации")]
        [MaxLength(80)]
        [Remote(action: "LocationInUse", controller: "Location")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите код")]
        public string GoogleCode { get; set; }
        [Required(ErrorMessage = "Введите код")]
        public string YandexCode { get; set; }
        [Required(ErrorMessage = "Введите код")]
        public string TwoGisCode { get; set; }
    }
}
