namespace Taxi_Database.Models
{
    public class Location
    {
        public Location(string nameOfLocation, string googleCode, string yandexCode, string twoGisCode)
        {
            NameOfLocation = nameOfLocation;
            GoogleCode = googleCode;
            YandexCode = yandexCode;
            TwoGisCode = twoGisCode;
        }

        public int Id { get; set; }
        public string NameOfLocation { get; set; }
        public string GoogleCode { get; set; }
        public string YandexCode { get; set; }
        public string TwoGisCode { get; set; }
    }
}
