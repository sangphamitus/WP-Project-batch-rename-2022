using System;
using System.Windows.Input;
using System.Windows;

namespace IRule
{
    public interface IRules
    {
        public static string ruleName { get; }

        string applyRule(string filename);
        IRules? parse(string data);
        void reset();
        public IRules? EditRule();

        public  bool isEditatble();
 
    }
    public interface IRuleEdit
    {
       
        public IRules getCurrentRule();

     
    }

}