using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Taxi_Database.Models
{
    public class User : IdentityUser
    {
        public ICollection<Message> Messages { get; set; }
        public User()
        {
            Messages = new HashSet<Message>();
        }
    }
}
