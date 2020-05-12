using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Services.Validation
{
    interface ICustomUserValidator
    {
        public List<IdentityError> Errors { get; }

        public bool Validate(string value, PatternClass field)
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
