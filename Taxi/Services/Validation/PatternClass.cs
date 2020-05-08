using System.Collections.Generic;

namespace Services.Validation
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
