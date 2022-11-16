using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_batch_rename_2022
{
    internal interface Rule_5_6_7
    {
        String applyRule(String filename);
    }

    internal class addPrefix : Rule_5_6_7
    {
        public addPrefix(String pre)
        {
            this.prefix = pre;
        }
        public string prefix { get; set; }
        public string applyRule(string filename)
        {
            filename = this.prefix + filename;
            return filename;
        }
    }

    internal class addSuffix : Rule_5_6_7
    {

        public addSuffix(String suf)
        {
            this.suffix = suf;
        }
        public string suffix { get; set; }
        public string applyRule(string filename)
        {
            filename = filename + this.suffix;
            return filename;
        }
    }

    internal class toLowerCase : Rule_5_6_7
    {

        public toLowerCase()
        {

        }
        public string applyRule(string filename)
        {
            filename = Regex.Replace(filename, @"[A-Z]", m => m.ToString().ToLower());
            return filename;
        }
    }
}
