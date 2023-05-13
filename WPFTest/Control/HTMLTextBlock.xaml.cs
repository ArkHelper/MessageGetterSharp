using HtmlAgilityPack;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
using System.Xml;

namespace WPFDemo.Control
{
    /// <summary>
    /// HTMLTextBlock.xaml 的交互逻辑
    /// </summary>
    partial class HTMLTextBlock : TextBlock
    {
        public static readonly DependencyProperty HTMLProperty =
        DependencyProperty.Register(
            "HTML",
            typeof(string),
            typeof(HTMLTextBlock),
            new FrameworkPropertyMetadata(string.Empty, OnHTMLPropertyChanged));

        public string HTML
        {
            get { return (string)GetValue(HTMLProperty); }
            set { SetValue(HTMLProperty, value); }
        }

        private static void OnHTMLPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var htmlTextBlock = (HTMLTextBlock)d;
            var newHTML = (string)e.NewValue;

            // 在这里执行属性更改的逻辑
            RenderHtml(htmlTextBlock,newHTML);
        }

        public HTMLTextBlock()
        {
            InitializeComponent();
        }
        private static void RenderHtml(TextBlock textBlock, string html)
        {
            textBlock.Inlines.Clear();

            // Create an HtmlDocument object
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            InlineCollection inlineCollectionOfHyperLinkUINeedToAddInline = null;
            foreach (HtmlNode node in htmlDoc.DocumentNode.Descendants())
            {
                switch (node.NodeType)
                {
                    case HtmlNodeType.Text:
                        string text = node.InnerText;

                        if (inlineCollectionOfHyperLinkUINeedToAddInline == null)
                        {
                            if (!string.IsNullOrWhiteSpace(text))
                            {
                                textBlock.Inlines.Add(new Run() { Text = text });
                            }
                        }
                        else
                        {
                            inlineCollectionOfHyperLinkUINeedToAddInline.Add(text);
                            inlineCollectionOfHyperLinkUINeedToAddInline = null;
                        }
                        break;

                    case HtmlNodeType.Element:
                        switch (node.Name)
                        {
                            case "a":
                                var href = node.Attributes["href"].Value;
                                var hyper = new Hyperlink();
                                hyper.Click += (ss, es) =>
                                {
                                    try
                                    {
                                        Process.Start(new ProcessStartInfo
                                        {
                                            FileName = href,
                                            UseShellExecute = true
                                        });
                                    }
                                    catch
                                    {

                                    }
                                };
                                inlineCollectionOfHyperLinkUINeedToAddInline = hyper.Inlines;
                                textBlock.Inlines.Add(hyper);
                                textBlock.Inlines.Add(new Run() { Text = " " });
                                break;

                            case "br":
                                textBlock.Inlines.Add(new LineBreak());
                                break;

                            default:
                                // 不处理其他元素
                                break;
                        }
                        break;

                    case HtmlNodeType.Comment:
                        // 不处理注释节点
                        break;
                }
            }
        }
    }
}
