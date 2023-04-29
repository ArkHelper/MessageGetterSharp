using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class Configuration
    {
        private string _rootDir;
        private bool _downloadMediaWhenGetMessages;
        private int _interval;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Configuration()
        {
            _rootDir = string.Empty;
            _downloadMediaWhenGetMessages = false;
            _interval = 60000;
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string RootDir
        {
            get { return _rootDir; }
            set { _rootDir = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 重复更新器频率，单位毫秒
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; OnPropertyChanged(); }
        }

        public bool DownloadMediaWhenGetMessages
        {
            get { return _downloadMediaWhenGetMessages; }
            set { _downloadMediaWhenGetMessages = value; OnPropertyChanged(); }
        }
    }
}
