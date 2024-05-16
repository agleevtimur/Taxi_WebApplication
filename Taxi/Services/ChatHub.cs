using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Taxi_Database.Models;

namespace Services
{
    public class ChatHub : Hub
    {
        public async Task Send(Message message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
