using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class Configuration
    {
        private string _rootAddress;
        

        public Configuration() 
        {
            _rootAddress = string.Empty;
        }

        public string RootAddress
        {
            get { return _rootAddress; }
            set { _rootAddress = value; }
        }
    }
}
