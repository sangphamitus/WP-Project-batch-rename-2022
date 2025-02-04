﻿using IRule;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;

namespace PascalCase
{
    public class PascalCase : IRules
    {
        public PascalCase()
        {
           
        }

        public IRules? parse(string data) => new PascalCase();


   
        public string applyRule(string filename,string type)
        {

            int index = filename.LastIndexOf('.');
            string name = "", extension = "";
            if (index != -1 && type=="File")
            {
                name = filename.Substring(0, index);
                extension = filename.Substring(index);
            }
            else name = filename;
            Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            Regex whiteSpace = new Regex(@"(?<=\s)");
            Regex startsWithLowerCaseChar = new Regex("^[a-z]");
            Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");
            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(name, "_"), string.Empty)
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
            string result = string.Concat(pascalCase)+extension;
           
            return result;
        }

        public void reset()
        {
            return;
        }
        public static string ruleName { get => "Pascal Case"; }

    

        public bool isEditatble()
        {
            return false;
        }

        public override string ToString()
        {
            return "To PascalCase";
        }

        IRules? IRules.EditRule()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string toJSON()
        {
            var obj = new 
            {

                ruleName = ruleName,
            };
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(obj, options);
            return jsonString;
        }

        public bool importPreset(JSONruleFile preset)
        {
            try
            {
                
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
    }
}