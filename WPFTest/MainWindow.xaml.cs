using MessageGetter;
using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            Getter.Configuration = new Configuration()
            {
                AutoDownloadVideo = false,
                AutoDownloadPicture = true,
                Interval = 60000,
                RootDir = Directory.GetCurrentDirectory() + @"\message"
                
            };

            Getter.AddUser(new Weibo("6279793937"));
            Getter.NewMessageAdded += Getter_NewMessageAdded;
            Getter.StartInterval();
        }

        private void Getter_NewMessageAdded(Message message, MessageInfo messageInfo)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ListBox listBox = new ListBox();
                message.Medias.ForEach(m =>
                {
                    TextBlock textBlock = new TextBlock();
                    if (m.GetType() == typeof(Picture))
                    {
                        textBlock.Inlines.Add("图片：" + m.ID + ": ");

                        Run run = new Run();
                        run.Text = "0";

                        if (m.Local != null)
                        {
                            run.Text = "文件已存在，不再下载";
                        }
                        else
                        {
                            m.DownloadProgressChanged += (s, e) =>
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    run.Text = e.ProgressPercentage.ToString();
                                });
                            };
                            m.DownloadCompleted += (s, e) =>
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    run.Text = "下载结束";
                                });
                            };
                            m.Download();
                        }
                        textBlock.Inlines.Add(run);
                    }
                    if (m.GetType() == typeof(Video))
                    {

                        textBlock.Inlines.Add("视频：" + m.ID + "：不下载");
                    }

                    listBox.Items.Add(new ListBoxItem() { Content = textBlock });
                });

                this.box.Children.Add(
                    new WrapPanel()
                    {
                        Children =
                        {

                            new TextBlock()
                            {
                                Text = message.Text
                            },
                            listBox
                        },

                    }) ;
            });


        }
    }
}
