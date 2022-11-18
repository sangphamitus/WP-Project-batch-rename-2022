using IRule;

namespace RemoveSpace
{
    public class RemoveSpace : IRules
    {
        public RemoveSpace()
        {
        }

        public string applyRule(string filename,string type)
        {
          
                int index = filename.LastIndexOf('.');
                string name = "";string extension = "";
                if (index != -1 && type=="File")
                {
                    name = filename.Substring(0, index);
                    extension = filename.Substring(index);
                }
                else name = filename;
                string result = name.Trim() + extension;
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
      
        public bool isEditatble()
        {
            return false;
        }

        IRules? IRules.EditRule()
        {
            throw new NotImplementedException();
        }
    }
}