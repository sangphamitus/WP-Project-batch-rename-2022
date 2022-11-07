using IRule;

namespace AddSuffix
{
    public class AddSuffix : IRules
    {
        public AddSuffix()
        {
            this.suffix = "";
        }
        public AddSuffix(string suf)
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
      

        public IRules? parse(string data)
        {
           return new AddSuffix(data);
        }
        public static string ruleName { get => "Add Suffix"; }


     
    }
}