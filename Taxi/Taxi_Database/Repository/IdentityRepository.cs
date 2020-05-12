using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi_Database.Models;

namespace Taxi_Database.Repository
{
    public class RolesRepository : IRole
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesRepository(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public List<IdentityRole> GetRoles()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<IdentityResult> CreateRole(string name)
        {
            var result = await roleManager.CreateAsync(new IdentityRole(name));
            return result;
        }

        public async Task<IdentityRole> FindRole(string id)
        {
           var result = await roleManager.FindByIdAsync(id);
           return result;
        }

        public async Task<IdentityResult> DeleteRole(IdentityRole role)
        {
            var result = await roleManager.DeleteAsync(role);
            return result;
        }
    }

    public class UserRepository : IUser
    {
        private readonly UserManager<User> userManager;

        public UserRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public List<User> GetUsers()
        {
            return userManager.Users.ToList();
        }

        public async Task<User> FindUser(string userId)
        {
            var result = await userManager.FindByIdAsync(userId);
            return result;
        }

        public async Task<IList<string>> GetRoles(User user)
        {
            var result = await userManager.GetRolesAsync(user);
            return result;
        }

        public async Task AddRoles(User user, IEnumerable<string> addedRoles)
        {
            await userManager.AddToRolesAsync(user, addedRoles);
        }

        public async Task RemoveRoles(User user, IEnumerable<string> removedRoles)
        {
            await userManager.RemoveFromRolesAsync(user, removedRoles);
        }

        public async Task<IdentityResult> CreateUser(User user, string password = null)
        {
            var result = await userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(User user)
        {
            var result = await userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> DeleteUser(User user)
        {
            var result = await userManager.DeleteAsync(user);
            return result;
        }

        public async Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword)
        {
            var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmail(User user, string code)
        {
            var result = await userManager.ConfirmEmailAsync(user, code);
            return result;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<User> FindUserByName(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            return user;
        }

        public async Task<bool> IsConfirmEmail(User user)
        {
            var result = !await userManager.IsEmailConfirmedAsync(user);
            return result;
        }

        public async Task<string> GeneratePassword(User user)
        {
            var result = await userManager.GeneratePasswordResetTokenAsync(user);
            return result;
        }

        public async Task<IdentityResult> ResetPassword(User user, string code, string password)
        {
            var result = await userManager.ResetPasswordAsync(user, code, password);
            return result;
        }

        public async Task AddLogin(User user, ExternalLoginInfo info)
        {
            await userManager.AddLoginAsync(user, info);
        }
    }
}
