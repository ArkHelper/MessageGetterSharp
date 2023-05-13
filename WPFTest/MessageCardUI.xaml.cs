using MessageGetter;
using System;
using System.Collections.Generic;
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
    /// MessageCardUI.xaml 的交互逻辑
    /// </summary>
    public partial class MessageCardUI : UserControl
    {
        public static readonly DependencyProperty BindingPersonProperty =
        DependencyProperty.Register("BindingMessage", typeof(Message), typeof(UserControl), new PropertyMetadata(null));

        public Message BindingMessage
        {
            get { return (Message)GetValue(BindingPersonProperty); }
            set { SetValue(BindingPersonProperty, value); }
        }

        public MessageCardUI(Message message)
        {
            InitializeComponent();
            DataContext = message;
        }
    }
}
