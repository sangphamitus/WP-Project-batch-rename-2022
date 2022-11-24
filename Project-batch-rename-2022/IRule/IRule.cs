using System;
using System.Windows.Input;
using System.Windows;

namespace IRule
{
    public class JSONruleFile
    {
        public string ruleName { get; set; }
        public int NumberOfDigit { get; set; }
        public int Step { get; set; }
        public int StartValue { get; set; }
        public int _counter { get; set; }
        public string _prefix { get; set; }
        public string _suffix { get; set; }
        public string _extension { get; set; }
        public string _newC { get; set; }
        public string _oldC { get; set; }
    }

    public class JSONworkBench
    {
        public string Filename { get; set; }
        public string NewFilename { get; set; }
        public string Pathname { get; set; }
        public string Result { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
    }

    public interface IRules : ICloneable
    {
        public static string ruleName { get; }

        string applyRule(string filename,string type);
        IRules? parse(string data);
        void reset();
        public IRules? EditRule();

        public  bool isEditatble();
        public object Clone();

        public string toJSON();

        public bool importPreset(JSONruleFile preset);

    }
    public interface IRuleEdit
    {
       
        public IRules getCurrentRule();

     
    }

}