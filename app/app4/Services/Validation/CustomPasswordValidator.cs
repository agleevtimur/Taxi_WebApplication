using app4.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app4.Services.Validation
{
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser>, ICustomUserValidator
        where TUser:IdentityUser
    {
        public List<IdentityError> Errors { get; } = new List<IdentityError>();

        PatternClass validation { get; } = new PatternClass()
        {
            Mistake = { 
                        { @"(.)\1{4,}", "Пароль не должен включать повторяющиеся подряд символы" }
                      },
            Pattern = { 
                        { @".*[A-Z].*","Пароль должен включать хотябы 1 заглавную букву" },
                        {  @".*[0-9].*","Пароль не достаточно надежен, используйте цифры"},
                        {@".*\W.*", "Пароль не достаточно надежен, попробуйте включить специальные символы"  }  
                      }
        };
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            ICustomUserValidator validator = new CustomPasswordValidator<TUser>();
            validator.Validate(password, validation);
            return Task.FromResult(validator.Errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(validator.Errors.ToArray()));
            //TO Do переопрделить валидацию пароля от Identity
        }
    }
}
