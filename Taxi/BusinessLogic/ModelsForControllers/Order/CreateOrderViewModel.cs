using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ModelsForControllers
{
    public class CreateOrderViewModel
    {
        public List<string> LocationsFrom { get; set; }
        public string LocationFrom { get; set; }
        public List<string> LocationsTo { get; set; }
        public string LocationTo { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public int CountOfPeople { get; set; }
    }
}
