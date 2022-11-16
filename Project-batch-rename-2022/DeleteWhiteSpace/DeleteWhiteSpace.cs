using IRule;

namespace DeleteWhiteSpace
{
    public class DeleteWhiteSpace : IRules
    {
        public DeleteWhiteSpace()
        {
        
        }
        public string applyRule(string filename, string type)
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
     

        public IRules? parse(string data)
        {
            return new DeleteWhiteSpace();
        }

        public static string ruleName { get => "Remove White Space"; }


    
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