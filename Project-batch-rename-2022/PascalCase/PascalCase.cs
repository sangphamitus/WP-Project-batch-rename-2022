using IRule;
using System.Text.RegularExpressions;

namespace PascalCase
{
    public class PascalCase : IRules
    {
        public PascalCase()
        {
           
        }

        public IRules? parse(string data) => new PascalCase();


   
        public string applyRule(string filename)
        {
            string[] parts = filename.Split('.');
            Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            Regex whiteSpace = new Regex(@"(?<=\s)");
            Regex startsWithLowerCaseChar = new Regex("^[a-z]");
            Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");
            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(parts[0], "_"), string.Empty)
                // split by underscores
                .Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                // set first letter to uppercase
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                // replace second and all following upper case letters to lower if there is no next lower (ABC -> Abc)
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                // set upper case the first lower case following a number (Ab9cd -> Ab9Cd)
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                // lower second and next upper case letters except the last if it follows by any lower (ABcDEf -> AbcDef)
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));
            string result = string.Concat(pascalCase);
            if (parts.Length > 1) result = result + "." + parts[1];
            return result;
        }

        public void reset()
        {
            return;
        }
        public static string ruleName { get => "Pascal Case"; }


        public override string ToString()
        {
            return "To PascalCase";
        }
    }
}