using IRule;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;

namespace LowerCase
{
    public class LowerCase : IRules,ICloneable
    {
        public LowerCase()
        {

        }
        public string applyRule(string filename,string type)
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
    

        public IRules? parse(string data)
        {
            return new LowerCase();
        }

       
        public bool isEditatble()
        {
            return false;
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

                ruleName= ruleName,
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

        public static string ruleName { get => "Lower Case"; }


    
    }
}