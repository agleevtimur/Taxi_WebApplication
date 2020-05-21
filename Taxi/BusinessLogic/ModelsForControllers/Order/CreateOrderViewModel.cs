using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ModelsForControllers
{
    public class CreateOrderViewModel
    {
        public string Id { get; set; }
        public List<string> LocationsFrom { get; set; }
        [Required]
        public string LocationFrom { get; set; }
        public List<string> LocationsTo { get; set; }
        [Required]
        public string LocationTo { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public int CountOfPeople { get; set; }
    }
}
