using IRule;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_batch_rename_2022
{
    internal class RuleFactory
    {
        private static RuleFactory instance = null;
        Dictionary<string, IRules> _property;
        RuleFactory()
        {

           _property = new Dictionary<string, IRules>();
        }

        public static RuleFactory NewInstance()
        {
            if(instance==null)
            {
                instance= new RuleFactory();

            }
            return instance;    
        }
        public void Inject(string key, IRules rule)
        {
            _property.Add(key, rule);

        }
        public IRules? rules(string key) { 
            return _property[key]; 
        }
        public BindingList<string> listKeys()
        {
            BindingList<string> result = new BindingList<string>();
            Dictionary<string, IRules>.KeyCollection keys = _property.Keys;
            foreach (string key in keys)
            {
                result.Add(key);
            }
            return result;
        }
    }
}
