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
        private bool _autoDownloadPicture;
        private bool _autoDownloadVideo;
        private int _interval;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Configuration()
        {
            _rootDir = string.Empty;
            _autoDownloadPicture = true;
            _autoDownloadVideo = false;
            _interval = 60000;
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 库工作的根目录
        /// </summary>
        public string RootDir
        {
            get { return _rootDir; }
            set { _rootDir = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 重复更新器频率，单位毫秒，默认为<see cref="int">60000</see>
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; OnPropertyChanged(); }
        }
        
        /// <summary>
        /// 获取消息后立即开始下载其中的视频，默认为<see cref="bool">False</see>
        /// </summary>
        public bool AutoDownloadVideo
        {
            get { return _autoDownloadVideo; }
            set { _autoDownloadVideo = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 获取消息后立即开始下载其中的图片，默认为<see cref="bool">True</see>
        /// </summary>
        public bool AutoDownloadPicture
        {
            get { return _autoDownloadPicture; }
            set { _autoDownloadPicture = value; OnPropertyChanged(); }
        }
    }
}
