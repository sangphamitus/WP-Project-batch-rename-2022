using IRule;

namespace AddPrefix
{
    public class AddPrefix : IRules
    {

       private string _prefix = "";
        public AddPrefix()
        {
            

         }
        public AddPrefix(string pre)
        {
            this._prefix = pre;
        }
       
        public string applyRule(string filename)
        {
            filename = this._prefix + filename;
            return filename;
        }

        public void reset()
        {
            return;
        }

        public override string ToString()
        {
            return "Add Prefex: " + this._prefix;
        }
      
        public IRules? parse(string data)
        {
            return new AddPrefix(data);
        }

        public static string ruleName { get => "Add Prefix"; }

       
    }
}