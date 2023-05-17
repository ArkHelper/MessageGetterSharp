using MessageGetter;
using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
using WPFDemo.RuntimeStorageHelper;

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

            if (message.Repost != null)
                this.repostMessageDock.Children.Add(new MessageCardUIContent(message.Repost));
            else
                this.repostMessageDock.Visibility = Visibility.Collapsed;

            var UserAvaClip = new RectangleGeometry()
            {
                RadiusY = 18,
                RadiusX = 18,
                Rect = new Rect(0, 0, 36, 36)
            };
            UserAvatar.Children.Add(
                new PictureViewer(message.User.Avatar)
                {
                    IsEnabled = false,
                    Width = 36,
                    Height = 36,
                    Clip = UserAvaClip
                });
            if (message.Medias.Exists(t=>t is Picture))
                Picture.Children.Add(
                    new PictureViewer((Picture)message.Medias[0])
                    {
                        Width = 100,
                        Height = 100
                    });
        }

        public MessageCardUIContent()
        {
        }
    }
}
