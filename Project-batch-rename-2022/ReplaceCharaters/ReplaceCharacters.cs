

using IRule;

namespace ReplaceCharaters
{
    public class ReplaceCharacters : IRules
    {
        string _oldChar = ".";
        string _newChar = ".";

        public ReplaceCharacters()
        {
        

        }
        ReplaceCharacters(string oldChar,string newChar)
        {
            this._oldChar = oldChar;
            this._newChar = newChar;
        }
        public IRules? parse(string data)
        {

            // [oldchar]\\[newchar]
            string[] parts = data.Split('\\');
            return new ReplaceCharacters(parts[0], parts[1]);
        }

        public string applyRule(string filename)
        {
            if (this._newChar == ".") return filename;
            string[] parts = filename.Split('.');
            string result = parts[0].Replace(this._oldChar, this._newChar);
            if (parts.Length > 1) result = result + "." + parts[1];
            return result;
        }

        public void reset()
        {
            return;
        }

        public override string ToString()
        {
            return "Replace char: " + this._oldChar + " with: " + this._newChar;
        }

        public static string ruleName { get => "Replace Character"; }

    
    }
}