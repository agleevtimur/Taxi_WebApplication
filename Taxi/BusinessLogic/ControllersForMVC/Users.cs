using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Users : IUserController
    {
        private readonly UserManager<User> userManager;
        public Users(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public List<User> Index()
        {
            IUser repository = new UserRepository(userManager);
            var model = repository.GetUsers();
            return model;
        }

        public async Task<IdentityResult> Create(string email, string login, string password)
        {
            IUser repository = new UserRepository(userManager);
            User user = new User { Email = email, UserName = login };
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
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Login = user.UserName };
            return model;
        }

        public async Task<IdentityResult> EditPost(User user, EditUserViewModel model)
        {
            IUser repository = new UserRepository(userManager);
            user.Email = model.Email;
            user.UserName = model.Login;
            var result = await repository.UpdateUser(user);
            return result;
        }

        public async Task<IdentityResult> Delete(string id)
        {
            IUser repository = new UserRepository(userManager);
            User user = await repository.FindUser(id);
            if (user != null)
            {
                IdentityResult result = await repository.DeleteUser(user);
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
            IdentityResult result =
                        await repository.ChangePassword(user, oldPassword, newPassword);
            return result;
        }
    }
}
