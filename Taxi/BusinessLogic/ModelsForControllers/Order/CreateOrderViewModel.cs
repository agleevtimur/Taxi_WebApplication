using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ModelsForControllers
{
    public class CreateOrderViewModel
    {
        public string Id { get; set; }
        public List<string> LocationsFrom { get; set; }
        [Required]
        [Remote(action: "StartIsDifferent", controller: "Order", AdditionalFields = nameof(LocationTo))]
        public string LocationFrom { get; set; }
        public List<string> LocationsTo { get; set; }
        [Required]
        [Remote(action: "DestinationIsDifferent", controller: "Order", AdditionalFields = nameof(LocationFrom))]
        public string LocationTo { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        //[Range(1, 3, ErrorMessage = "Недопустимое количество")]
        public int CountOfPeople { get; set; }
    }
}
