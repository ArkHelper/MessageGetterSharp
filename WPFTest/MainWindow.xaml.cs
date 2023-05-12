﻿using MessageGetter;
using MessageGetter.Medias;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            /* 设置配置 */
            Getter.Configuration = new Configuration()
            {
                AutoDownloadVideo = false, //设置不自动下载视频
                AutoDownloadPicture = true, //设置自动下载图片
                Interval = 60000, //设置刷新器定时（单位：毫秒）
                RootDir = Directory.GetCurrentDirectory() + @"\message", //设置文件保存根目录

                /* 设置筛选条件（不符合筛选条件的也将进入队列，但是没有新消息提醒
                 * 此处筛选条件是通过传入一个[具有一个形参一个输出的函数]实现的
                 * 当符合筛选条件（即保留该消息）要返回True。*/
                Filter = new System.Func<Message, bool>((message) =>
                {
                    bool @in = true;

                    if (message.Text.Contains("微博抽奖平台")) @in = false;
                    return true;
                })

            };

            Getter.AddUser(new Weibo("6279793937")); //添加用户
            Getter.NewMessageAdded += Getter_NewMessageAdded; //绑定事件
            Getter.StartInterval();//启动刷新
        }

        private void Getter_NewMessageAdded(Message message, MessageInfo messageInfo)
        {
            Application.Current.Dispatcher.Invoke(() => MessageBoxs.Children.Add(new MessageCardUI(message)));
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                message.Text = "666666";
            });
        }
    }
}
