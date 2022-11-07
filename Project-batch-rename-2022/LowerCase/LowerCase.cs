using IRule;
using System.Text.RegularExpressions;

namespace LowerCase
{
    public class LowerCase : IRules
    {
        public LowerCase()
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
    

        public IRules? parse(string data)
        {
            return new LowerCase();
        }


        public static string ruleName { get => "Lower Case"; }


    
    }
}