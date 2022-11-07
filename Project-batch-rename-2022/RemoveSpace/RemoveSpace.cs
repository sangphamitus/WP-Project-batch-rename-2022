using IRule;

namespace RemoveSpace
{
    public class RemoveSpace : IRules
    {
        public RemoveSpace()
        {
        }

        public string applyRule(string filename)
        {
            string[] parts = filename.Split('.');

            string result = parts[0].Trim();
            if (parts.Length > 1) result = result + "." + parts[1];
            return result;
        }

        public void reset()
        {
            return;
        }
        public IRules? parse(string data)
        {
            return new RemoveSpace();
        }
        public static string ruleName { get => "Remove Space"; }
       
        public override string ToString()
        {
            return "Remove Space";
        }
    }
}