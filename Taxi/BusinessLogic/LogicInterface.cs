﻿using BusinessLogic.ModelsForControllers;
using BusinessLogic.ModelsForControllers.Home;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        Task<Client> Index(string id);
        ClientAuthorizeViewModel Information(Client client);
        IEnumerable<Client> Clients();
        Task<IdentityResult> Create(string email, string login, string password);
        Task<User> FindUser(string id);
        Task<EditUserViewModel> EditGet(User user);
        Task<IdentityResult> EditPost(User user, EditUserViewModel model);
        Task<IdentityResult> Delete(string id);
        ChangePasswordViewModel ChangeGet(string id);
        Task<IdentityResult> ChangePost(User user, string oldPassword, string newPassword);
        Task Subscription(int priority, int countOfTravels, string id);
    }

    public interface IAccountController
    {
        User RegisterGet(string email, string userName);
        Task<IdentityResult> Create(User user, string password);
        Task<string> Register(User user, string password);
        Client GetUserByLogin(string login);
        Task<User> FindUser(string userId);
        Task<IdentityResult> ConfirmGet(User user, string code);
        Task<LoginViewModel> LoginGet(string returnUrl = null);
        Task<Microsoft.AspNetCore.Identity.SignInResult> LoginPost(LoginViewModel model);
        Task LogOut();
        Task<User> EmailInUse(string email);
        Task<User> UserNameInUse(string userName);
        Task<IdentityResult> PasswordIsStrong(string password);
        Task<bool> IsConfirmEmail(User user);
        Task<string> Generate(User user);
        Task SendEmail(string email, string callbackUrl);
        Task<IdentityResult> ResetPassword(User user, string code, string password);
        ChallengeResult ExternalLogin(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetInfo();
        Task<Microsoft.AspNetCore.Identity.SignInResult> GetSignInResult(ExternalLoginInfo info);
        string GetEmail(ExternalLoginInfo info);
        Task<IdentityResult> LoginUser(ExternalLoginInfo info);
        Task ExternalLoginCallback(User user, ExternalLoginInfo info);
    }

    public interface IOrderController
    {
        IndexOrderViewModel Index();
        CreateOrderViewModel CreateGet(string id);
        Task Create(string locationFrom, string locationTo, string time, int countOfPeople, string id);
        OrderWithClientViewModel GetOrder(int id);
        List<ReadyOrders> GetOrdersByClientId(string id);
        IEnumerable<Order> GetRequestsByClientId(string id);
        ReadyOrders GetReadyOrderId(int id);
        Task DeleteOrder(int id);
        Task Rating(string whoId, string whomId, int orderId, int newRating);
    }

    public interface ILocationController
    {
        IEnumerable<Location> Index();
        IEnumerable<HistoryOfLocation> History();
        Task SavePost(string name, string googleCode, string yandexCode, string twoGisCode);
    }

    public interface IHomeController
    {
        IndexHomeViewModel Index();
    }
}
