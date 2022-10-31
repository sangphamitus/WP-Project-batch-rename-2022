using Project_batch_rename_2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_batch_rename_2022
{
    internal interface Rule_1_3_4
    {
        String applyRule(String filename);
    }


    internal class changeExtension : Rule_1_3_4
    {
        public changeExtension()
        {
            this.extension = ".";
        }
        public string extension { get; set; }
        public string applyRule(string filename)
        {
            return filename.Replace(filename.Substring(filename.LastIndexOf('.') + 1), this.extension);
        }
    }
    internal class removeSpace : Rule_1_3_4
    {

        public string applyRule(string filename)
        {
            string[] parts = filename.Split('.');

            return parts[0].Trim() + '.' + parts[1];
        }
    }
    internal class toPascalCase : Rule_1_3_4
    {

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

            return string.Concat(pascalCase) + '.' + parts[1];
        }
    }
    internal class replaceCharacters : Rule_1_3_4
    {
        public replaceCharacters()
        {
            this.oldChar = ".";
            this.newChar = ".";
        }
        public string oldChar { get; set; }
        public string newChar { get; set; }
        public string applyRule(string filename)
        {
            string[] parts = filename.Split('.');
            return parts[0].Replace(this.oldChar, this.newChar) + '.' + parts[1];
        }
    }

}
