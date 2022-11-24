using IRule;
using System.Text.Json;
using System.Windows;

namespace RemoveSpace
{
    public class RemoveSpace : IRules,ICloneable
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