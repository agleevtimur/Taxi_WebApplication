using app4.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace app4.Services
{
    interface ICustomUserValidator
    {
        public List<IdentityError> Errors { get; }

        public bool  Validate(string value, PatternClass field)
        {
            return method(value, field.Mistake, field.Pattern);
        }
        private bool method(string value, Dictionary<string, string> mistake, Dictionary<string, string> pattern)
        {
            foreach (var pat in mistake)
            {
                if (Regex.IsMatch(value, pat.Key))
                {
                    Errors.Add(new IdentityError { Description = pat.Value });
                    return false;
                }
            }
            foreach (var pat in pattern)
            {
                if (!Regex.IsMatch(value, pat.Key))
                {
                    Errors.Add(new IdentityError { Description = pat.Value });
                    return false;
                }
            }
            return true;
        }
    }
}
