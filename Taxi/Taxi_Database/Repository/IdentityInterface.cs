using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Taxi_Database.Models;

namespace Taxi_Database.Repository
{
    public interface IRole
    {
        List<IdentityRole> GetRoles();
        Task<IdentityResult> CreateRole(string name);
        Task<IdentityRole> FindRole(string id);
        Task<IdentityResult> DeleteRole(IdentityRole role);
    }

    public interface IUser
    {
        public List<User> GetUsers();
        Task<User> FindUser(string userId);
        Task<IList<string>> GetRoles(User user);
        Task AddRoles(User user, IEnumerable<string> addedRoles);
        Task RemoveRoles(User user, IEnumerable<string> removedRoles);
        Task<IdentityResult> CreateUser(User user, string password);
        Task<IdentityResult> UpdateUser(User user);
        Task<IdentityResult> DeleteUser(User user);
        Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword);
        Task<IdentityResult> ConfirmEmail(User user, string code);
        Task<User> FindUserByEmail(string email);
        Task<User> FindUserByName(string name);
        Task<bool> IsConfirmEmail(User user);
        Task<string> GeneratePassword(User user);
        Task<IdentityResult> ResetPassword(User user, string code, string password);
        Task AddLogin(User user, ExternalLoginInfo info);
    }

    public interface IChat
    {
        Task<User> GetUser(ClaimsPrincipal user);
    }
}
