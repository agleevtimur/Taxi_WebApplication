using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app4.Services
{
    public class PatternClass
    {
        public Dictionary<string, string> Mistake { get; } // то что не должно содержаться в строке
        public Dictionary<string, string> Pattern { get; } // то что должно

        public PatternClass()
        {
            Mistake = new Dictionary<string, string>();
            Pattern = new Dictionary<string, string>();
        }
    }
}
