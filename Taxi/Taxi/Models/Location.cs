using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string NameOfLocation { get; set; }
        public string GoogleCode { get; set; }
        public string YandexCode { get; set; }
        public string TwoGisCode { get; set; }
    }
}
