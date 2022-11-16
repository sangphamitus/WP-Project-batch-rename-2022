using IRule;
namespace ChangeExtension
{
    public class ChangeExtension : IRules
    {
        string _extension = ".";
        public ChangeExtension(string extension)
        {
            this._extension =extension;
        }

        public ChangeExtension()
        {
            this.extension = "abc";
        }

        public string extension { get; set; }
  
        public string applyRule(string filename, string type)
        {
            if(type=="Folder") return filename;
            string[] parts = filename.Split('.');
            string result = parts[0];
            if (parts.Length > 1) result = result + "." + this.extension;
            return result;
        }

        public void reset()
        {
            return;
        }


        public IRules? parse(string data)
        {
           
            return new ChangeExtension(data);
        }
      
        public bool isEditatble()
        {
            return true;
        }

        public static string ruleName { get => "Change Extension"; }


      
        public override string ToString()
        {
            return "Change Extension: " + this.extension;
        }

        IRules? IRules.EditRule()
        {
            throw new NotImplementedException();
        }
    }
}