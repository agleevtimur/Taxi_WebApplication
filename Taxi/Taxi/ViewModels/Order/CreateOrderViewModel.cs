using System;
using System.ComponentModel.DataAnnotations;

namespace Taxi.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public string[] LocationsFrom { get; set; }
        public string LocationFrom { get; set; }
        public string[] LocationsTo { get; set; }
        public string LocationTo { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int CountOfPeople { get; set; }
    }
}
