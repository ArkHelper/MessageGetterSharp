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
        private bool _initMediaManually;
        private int _interval;

        public Configuration() 
        {
            _rootAddress = string.Empty;
            _initMediaManually = false;
            _interval = 0;
        }

        public string RootAddress
        {
            get { return _rootAddress; }
            set { _rootAddress = value; }
        }

        /// <summary>
        /// 重复更新器频率，单位毫秒
        /// </summary>
        public int Interval
        { 
            get { return _interval; }
            set { _interval = value; }
        }

        public bool InitMediaManually
        {
            get { return _initMediaManually;}
            set { _initMediaManually = value;}
        }
    }
}
