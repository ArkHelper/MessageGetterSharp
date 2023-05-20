using MessageGetter;
using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mime;
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
using WPFDemo.Helper;

namespace WPFDemo.Control
{
    /// <summary>
    /// MessageCardUIContent.xaml 的交互逻辑
    /// </summary>
    public partial class MessageCardUIContent : UserControl
    {
        public static readonly DependencyProperty BindingMessageProperty =
        DependencyProperty.Register("BindingMessage", typeof(Message), typeof(MessageCardUIContent), new PropertyMetadata(null));

        public Message BindingMessage
        {
            get { return (Message)GetValue(BindingMessageProperty); }
            set { SetValue(BindingMessageProperty, value); }
        }

        public MessageCardUIContent(Message message)
        {
            InitializeComponent();
            BindingMessage = message;
            DataContext = BindingMessage;

            #region 转发
            if (message.Repost != null)
                this.repostMessageDock.Children.Add(new MessageCardUIContent(message.Repost));
            else
                this.repostMessageDock.Visibility = Visibility.Collapsed;
            #endregion

            #region 用户头像
            var UserAvaClip = new RectangleGeometry()
            {
                RadiusY = 18,
                RadiusX = 18,
                Rect = new Rect(0, 0, 36, 36)
            };
            UserAvatar.Children.Add(
                new PictureViewer(message.User.Avatar, false)
                {
                    IsEnabled = false,
                    Width = 36,
                    Height = 36,
                    Clip = UserAvaClip
                });
            #endregion

            #region 图片
            bool small = message.Medias.Count > 1;
            foreach (var item in message.Medias)
            {
                if (item is Video video)
                {
                    Picture.Children.Add(new PictureViewer(video.Cover, false) { Link = video.Link});
                }
                else
                {
                    Picture.Children.Add(
                        new PictureViewer(item as Picture, small));
                }
            }
            Picture.MaxWidth = (message.Medias.Count <= 3) ? 500 : 320;
            #endregion
        }

        public MessageCardUIContent()
        {
        }
    }
}
