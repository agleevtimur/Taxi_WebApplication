using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Users : IUserController
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationContext context;
        public Users(UserManager<User> userManager, ApplicationContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public Client Index(string id)
        {
            IRepository repository = new Repository(context);
            return repository.GetClient(id);
        }

        public ClientAuthorizeViewModel Information(Client client)
        {
            ClientAuthorizeViewModel model = new ClientAuthorizeViewModel
            {
                Id = client.StringId,
                AboutSelf = client.AboutSelf,
                ClientName = client.ClientName,
                CountOfTrips = client.CountOfTrips,
                Email = client.Email,
                FirstName = client.FirstName,
                LeftOrdersPriority = client.LeftOrdersPriority,
                Password = client.Password,
                Priority = client.Priority,
                Rating = client.Rating,
                RegisterTime = client.RegisterTime,
                SecondName = client.SecondName
            };
            return model;
        }

        public IEnumerable<Client> Clients()
        {
            IRepository repository = new Repository(context);
            var model = repository.GetClients();

            return model;
        }

        public async Task<IdentityResult> Create(string email, string login, string password)
        {
            IUser repository = new UserRepository(userManager);
            IRepository repository1 = new Repository(context);
            User user = new User { Email = email, UserName = login };
            await repository1.SaveClient(new Client(user.Id, null, null, user.UserName, user.Email, password,
                0, 0, 0, -1, null, 0, DateTime.Now));
            var result = await repository.CreateUser(user, password);
            return result;
        }

        public async Task<User> FindUser(string id)
        {
            IUser repository = new UserRepository(userManager);
            User user = await repository.FindUser(id);
            return user;
        }

        public EditUserViewModel EditGet(User user)
        {
            IRepository repository = new Repository(context);
            var client = repository.GetClient(user.Id);
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Login = user.UserName,
                FirstName = client.FirstName, SecondName = client.SecondName, AboutSelf = client.AboutSelf};
            return model;
        }

        public async Task<IdentityResult> EditPost(User user, EditUserViewModel model)
        {
            IUser repository = new UserRepository(userManager);
            IRepository repository1 = new Repository(context);
            var client = repository1.GetClient(model.Id);
            await repository1.EditClient(Update(model, client));
            user.Email = model.Email;
            user.UserName = model.Login;
            var result = await repository.UpdateUser(user);
            return result;
        }

        private Client Update(EditUserViewModel model, Client client)
        {
            client.FirstName = model.FirstName;
            client.SecondName = model.SecondName;
            client.Email = model.Email;
            client.ClientName = model.Login;
            client.AboutSelf = model.AboutSelf;
            return client;
        }

        public async Task<IdentityResult> Delete(string id)
        {
            IUser repository = new UserRepository(userManager);
            IRepository repository1 = new Repository(context);

            User user = await repository.FindUser(id);
            if (user != null)
            {
                IdentityResult result = await repository.DeleteUser(user);
                await repository1.DeleteClient(id);
                return result;
            }

            return null;
        }

        public ChangePasswordViewModel ChangeGet(string id)
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = id };
            return model;
        }

        public async Task<IdentityResult> ChangePost(User user, string oldPassword, string newPassword)
        {
            IUser repository = new UserRepository(userManager);
            IRepository repository1 = new Repository(context);
            IdentityResult result =
                        await repository.ChangePassword(user, oldPassword, newPassword);
            var client = repository1.GetClient(user.Id);
            client.Password = newPassword;
            await repository1.EditClient(client);
            return result;
        }
        
        public async Task Subscription(int priority, int countOfTravels, string id)
        {
            IRepository repository = new Repository(context);
            var client = repository.GetClient(id);
            client.Priority = priority;
            client.LeftOrdersPriority = countOfTravels;
            await repository.EditClient(client);
        }
    }
}
