using System;

namespace IRule
{
    public interface IRules
    {
        public static string ruleName { get; }

        string applyRule(string filename);
        IRules? parse(string data);
        void reset();
 
    }
}