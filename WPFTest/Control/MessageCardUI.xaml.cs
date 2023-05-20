﻿using System;
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

namespace WPFDemo.Control
{
    /// <summary>
    /// MessageCardUI.xaml 的交互逻辑
    /// </summary>
    public partial class MessageCardUI : UserControl
    {
        public MessageCardUI(Message message)
        {
            InitializeComponent();

            rootCard.Children.Add(new MessageCardUIContent(message));
            if (message.IsTop)
            {
                TopIcon.Visibility = Visibility.Visible;

            }
            else
            {
                TopIcon.Visibility = Visibility.Collapsed;
            }
        }
    }
}
