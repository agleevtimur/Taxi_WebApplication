using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Models;

namespace BusinessLogic
{
    public interface IRoleController
    {
        List<IdentityRole> Index();
        Task<IdentityResult> Create(string name);
        Task<IdentityResult> Delete(string id);
        List<User> UserList();
        Task<User> FindUser(string id);
        Task<ChangeRoleViewModel> EditGet(User user);
        Task EditPost(User user, List<string> roles);
    }

    public interface IUserController
    {
        List<User> Index();
        Task<IdentityResult> Create(string email, string login, string password);
        Task<User> FindUser(string id);
        EditUserViewModel EditGet(User user);
        Task<IdentityResult> EditPost(User user, EditUserViewModel model);
        Task<IdentityResult> Delete(string id);
        ChangePasswordViewModel ChangeGet(string id);
        Task<IdentityResult> ChangePost(User user, string oldPassword, string newPassword);
    }
}
