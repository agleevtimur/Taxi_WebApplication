using System;

namespace Taxi_Database.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string title, string message)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public string Title { get; set; }
        public string Message { get; set; }
    }
}