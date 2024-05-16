using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Taxi_Database.Models;

namespace Taxi_Database
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            LocalRole admin = new LocalRole("admin", "admin@gmail.com", "Aa123456!");
            LocalRole employee = new LocalRole("employee", "employee@gmail.com", "Aa1234567!");

            await Initialize(roleManager, userManager, admin);
            await Initialize(roleManager, userManager, employee);

        }
        private static async Task Initialize(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, LocalRole role)
        {
            if (await roleManager.FindByNameAsync(role.Name) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role.Name));
            }
            if (await userManager.FindByNameAsync(role.Name) == null)
            {
                User user = new User { Email = role.Email, UserName = role.Name };
                IdentityResult result = await userManager.CreateAsync(user, role.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
            }
        }
        private class LocalRole
        {
            public string Name { get; }
            public string Email { get; }
            public string Password { get; }
            public LocalRole(string name, string email, string password)
            {
                Name = name;
                Email = email;
                Password = password;
            }
        }
    }
}
