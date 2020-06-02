using Taxi_Database.Models;

namespace BusinessLogic.ControllersForMVC
{
    public class Error : IError
    {
        public Error()
        {

        }

        public ErrorViewModel GetError(string title, string text)
        {
            return new ErrorViewModel(title, text);
        }
    }
}
