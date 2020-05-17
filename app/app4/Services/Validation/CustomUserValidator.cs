using app4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace app4.Models
{
    public class CustomUserValidator<TUser> : IUserValidator<TUser>, 
        ICustomUserValidator, IRegisterValidation
        where TUser: IdentityUser
    {
        public List<IdentityError> Errors { get; } = new List<IdentityError>();
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            var validator = new CustomUserValidator<TUser>();
            ICustomUserValidator action = validator;
            IRegisterValidation register = validator;
            action.Validate(user.UserName, register.Login);
            action.Validate(user.Email, register.Email);

            return Task.FromResult(validator.Errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(validator.Errors.ToArray()));
        }
    }
}
