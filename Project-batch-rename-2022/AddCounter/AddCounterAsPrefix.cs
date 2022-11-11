using IRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AddCounter
{
    public class AddCounterAsPrefix : IRules
    {
        public AddCounterAsPrefix()
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
            result = generateCounter() + "_"+ parts[0];
            if (parts.Length > 1) result = result + "." + parts[1];
            return result;

        }
        public override string ToString()
        {
            return "Add Counter As Prefix";
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
            return new AddCounterAsPrefix();
        }

       

        public bool isEditatble()
        {
            return false;
        }

        IRules? IRules.EditRule()
        {
            throw new NotImplementedException();
        }

        public static string ruleName { get => "Add Counter As Prefix"; }
    }
}
