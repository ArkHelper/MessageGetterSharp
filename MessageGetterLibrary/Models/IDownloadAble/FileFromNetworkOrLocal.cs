using Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class FileFromNetworkOrLocal
    {
        public string? Link { get; set; }

        protected string? _expectedLocal;
        protected string? ExpectedLocal
        {
            get
            {
                return _expectedLocal;
            }
            set
            {
                _expectedLocal = value;
            }
        }
        public string? Local { get; set; } = null;

#pragma warning disable IDE0060 // 删除未使用的参数
        public FileFromNetworkOrLocal() { }
        /// <summary>
        /// 从链接构造多态文件，此行为认为文件在云端
        /// </summary>
        /// <param name="link">云端链接</param>
        /// <param name="Constructor_num">为了区分多态构造函数的占位形参，需要随意填写一个<see cref="int">double</see>类型量</param>
        public FileFromNetworkOrLocal(string link, int Constructor_num = 0)
        {
            Link = link;
        }

        /// <summary>
        /// 从路径构造多态文件，此行为认为文件在本地
        /// </summary>
        /// <param name="local"></param>
        /// <param name="Constructor_num">为了区分多态构造函数的占位形参，需要随意填写一个<see cref="double">double</see>类型量</param>
        public FileFromNetworkOrLocal(string local, double Constructor_num = 1)
        {
            Local = local;
        }
#pragma warning restore IDE0060 // 删除未使用的参数

        /// <summary>
        /// 从给定的链接开始下载该文件
        /// </summary>
        /// <param name="path">下载路径（可以包含文件名称）</param>
        /// <param name="evenIfExist">为<see cref="bool">true</see>时，将会覆盖本地已经存在的文件</param>
        /// <remarks>
        /// <list type="bullet">
        /// <item>订阅<see cref="DownloadProgressChanged">DownloadProgressChanged</see>事件在下载进度改变时执行动作</item>
        /// <item>订阅<see cref="DownloadCompleted">DownloadCompleted</see>事件在下载结束后执行动作</item>
        /// </list>
        /// </remarks>
        protected async Task Download(string? path, bool evenIfExist = false)
        {
            bool end = !evenIfExist && File.Exists(path);

            if (end)
            {
                var e1 = new AsyncCompletedEventArgs(null, false, true);
                OnDownloadFileCompleted(this, e1);
                return;
            }

            using DownloadService downloader = new();
            ExpectedLocal = path;

            downloader.DownloadProgressChanged += OnDownloadProgressChanged;
            downloader.DownloadFileCompleted += OnDownloadFileCompleted;

            await downloader.DownloadFileTaskAsync(Link, ExpectedLocal);
        }

        public event EventHandler<AsyncCompletedEventArgs>? DownloadCompleted;
        public event EventHandler<DownloadProgressChangedEventArgs>? DownloadProgressChanged;

        protected virtual void OnDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged?.Invoke(this, e);
        }

        protected virtual void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            DownloadCompleted?.Invoke(this, e);
        }
    }
}
