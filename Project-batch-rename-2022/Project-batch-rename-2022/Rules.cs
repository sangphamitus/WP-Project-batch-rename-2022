
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_batch_rename_2022
{
    internal interface Rules
    {
        String applyRule(String filename);
        void reset();
    }


    internal class ChangeExtension : Rules
    {
        public ChangeExtension()
        {
            this.extension = ".";
        }
        public string extension { get; set; }
        public string applyRule(string filename)
        {
            return filename.Replace(filename.Substring(filename.LastIndexOf('.') + 1), this.extension);
        }

        public void reset()
        {
            return;
        }

        public override string ToString()
        {
            return "Change Extension: " + this.extension;
        }

    }
    internal class removeSpace : Rules
    {

        public string applyRule(string filename)
        {
            string[] parts = filename.Split('.');

            return parts[0].Trim() + '.' + parts[1];
        }

        public void reset()
        {
            return;
        }

        public override string ToString()
        {
            return "Remove Space";
        }
        internal class toPascalCase : Rules
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

            public void reset()
            {
                return;
            }

            public override string ToString()
            {
                return "To PascalCase";
            }
        }
        internal class replaceCharacters : Rules
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

            public void reset()
            {
                return;
            }

            public override string ToString()
            {
                return "Replace char: " + this.oldChar + " with: " + this.newChar;
            }
        }
        internal class addCounter : Rules
        {
            public addCounter()
            {
                this.NumberOfDigit = 1;
                this.Step = 1;
                this.StartValue = 1;
                this._counter = 0;
            }
            public int NumberOfDigit { get; set; }
            public int Step { get; set; }
            public int StartValue { get; set; }
            private int _counter;
            private string generateCounter()
            {
                string result = (this.StartValue + this._counter * this.Step).ToString();
                while (result.Length < this.NumberOfDigit) { result = "0" + result; }
                this._counter++;
                return result;
            }
            public string applyRule(string filename)
            {
                string result;
                string[] parts = filename.Split('.');
                result = parts[0] + "_" + generateCounter() + "." + parts[1];
                return result;
            }
            public override string ToString()
            {
                return "Add Counter";
            }

            public void reset()
            {
                this.NumberOfDigit = 1;
                this.Step = 1;
                this.StartValue = 1;
                this._counter = 0;
            }
        }
        internal class deleteWhiteSpace : Rules
        {
            public string applyRule(string filename)
            {
                String result = "";
                for (int i = 0; i < filename.Length; i++)
                {
                    if (filename[i] != ' ') { result += filename[i]; }
                }

                return result;
            }

            public void reset()
            {
                return;
            }

            public override string ToString()
            {
                return "Remove White Space";
            }
        }
        internal class addPrefix : Rules
        {
            public addPrefix(String pre)
            {
                this.prefix = pre;
            }
            public string prefix { get; set; }
            public string applyRule(string filename)
            {
                filename = this.prefix + filename;
                return filename;
            }

            public void reset()
            {
                return;
            }

            public override string ToString()
            {
                return "Add Prefex: " + this.prefix;
            }
        }
        internal class addSuffix : Rules
        {

            public addSuffix(String suf)
            {
                this.suffix = suf;
            }
            public string suffix { get; set; }
            public string applyRule(string filename)
            {
                filename = filename + this.suffix;
                return filename;
            }

            public void reset()
            {
                return;
            }

            public override string ToString()
            {
                return "Add Suffix: " + this.suffix;
            }
        }
        internal class toLowerCase : Rules

        {

            public toLowerCase()
            {

            }
            public string applyRule(string filename)
            {
                filename = Regex.Replace(filename, @"[A-Z]", m => m.ToString().ToLower());
                return filename;
            }

            public void reset()
            {
                return;
            }

            public override string ToString()
            {
                return "To LowerCase";
            }
        }
    }
}
