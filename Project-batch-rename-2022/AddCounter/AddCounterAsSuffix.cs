using IRule;

namespace AddCounter
{
    public class AddCounterAsSuffix : IRules
    {
        public AddCounterAsSuffix()
        {
            this.NumberOfDigit = 1;
            this.Step = 1;
            this.StartValue = 1;
            this._counter = 0;
        }
        public int NumberOfDigit { get; set; }
        public int Step { get; set; }
        public int StartValue { get; set; }
        private int _counter;
        private string generateCounter()
        {
            string result = (this.StartValue + this._counter * this.Step).ToString();
            while (result.Length < this.NumberOfDigit) { result = "0" + result; }
            this._counter++;
            return result;
        }
        public string applyRule(string filename, string type)
        {
            int index = filename.LastIndexOf('.');
            string name = "", extension = "";
            if (index != -1 && type == "File")
            {
                name = filename.Substring(0, index);
                extension = filename.Substring(index);
            }
            else name = filename;
           
            string result = name + "_" + generateCounter()+extension;
           
            return result;

        }
        public override string ToString()
        {
            return "Add Counter As Suffix";
        }

        public void reset()
        {
            this.NumberOfDigit = 1;
            this.Step = 1;
            this.StartValue = 1;
            this._counter = 0;
        }
      
        public IRules? parse(string data)
        {
            return new AddCounterAsSuffix();
        }
      

        public bool isEditatble()
        {
            return false;
        }

        IRules? IRules.EditRule()
        {
            throw new NotImplementedException();
        }

      

        public static string ruleName { get => "Add Counter As Suffix"; }
       
    }
}