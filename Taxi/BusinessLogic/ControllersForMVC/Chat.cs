using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Chat : IChatController
    {
        private readonly IdentityContext context;
        private readonly UserManager<User> userManager;

        public Chat(IdentityContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<User> GetUser(ClaimsPrincipal user)
        {
            IChat repository = new ChatRepository(userManager);
            return await repository.GetUser(user);
        }

        public async Task<List<Message>> GetMessages()
        {
            return await context.Messages.ToListAsync();
        }

        public async Task Messages(ClaimsPrincipal user, Message message)
        {
            IChat repository = new ChatRepository(userManager);
            var sender = await repository.GetUser(user);
            message.UserId = sender.Id;
            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();
        }
    }
}
