using BusinessLogic.ModelsForControllers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Role : IRoleController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public Role(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public List<IdentityRole> Index()
        {
            IRole repository = new RolesRepository(roleManager);
            return repository.GetRoles();
        }

        public async Task<IdentityResult> Create(string name)
        {
            IRole repository = new RolesRepository(roleManager);
            return await repository.CreateRole(name);
        }

        public async Task<IdentityResult> Delete(string id)
        {
            IRole repository = new RolesRepository(roleManager);

            IdentityRole role = await repository.FindRole(id);
            if (role != null)
            {
                IdentityResult result = await repository.DeleteRole(role);
                return result;
            }
            
            return null;
        }

        public List<User> UserList()
        {
            IUser repository = new UserRepository(userManager);
            return repository.GetUsers();
        }

        public async Task<User> FindUser(string id)
        {
            IUser repository = new UserRepository(userManager);
            User user = await repository.FindUser(id);
            return user;
        }

        public async Task<ChangeRoleViewModel> EditGet(User user)
        {
            IRole repository1 = new RolesRepository(roleManager);
            IUser repository2 = new UserRepository(userManager);

            // получем список ролей пользователя
            var userRoles = await repository2.GetRoles(user);
            var allRoles = repository1.GetRoles();
            
            return GetModel(user.Id, user.Email, userRoles, allRoles);
        }

        private ChangeRoleViewModel GetModel (string id, string email, IList<string> roles, List<IdentityRole> allRoles)
        {
            var model = new ChangeRoleViewModel
            {
                UserId = id,
                UserEmail = email,
                UserRoles = roles,
                AllRoles = allRoles
            };
            return model;
        }

        public async Task EditPost(User user, List<string> roles)
        {
            IRole repository1 = new RolesRepository(roleManager);
            IUser repository2 = new UserRepository(userManager);
            var userRoles = await repository2.GetRoles(user);
            // получаем все роли
            var allRoles = repository1.GetRoles();
            // получаем список ролей, которые были добавлены
            var addedRoles = roles.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(roles);

            await repository2.AddRoles(user, addedRoles);

            await repository2.RemoveRoles(user, removedRoles);

        }
    }
}
