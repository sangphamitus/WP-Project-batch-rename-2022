using IRule;

namespace AddCounter
{
    public class AddCounter : IRules
    {
        public AddCounter()
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
        public string applyRule(string filename)
        {
            string result;
            string[] parts = filename.Split('.');
            result = parts[0] + "_" + generateCounter();
            if (parts.Length > 1) result = result + "." + parts[1];
            return result;

        }
        public override string ToString()
        {
            return "Add Counter";
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
            return new AddCounter();
        }

        public static string ruleName { get => "Add Counter"; }
       
    }
}