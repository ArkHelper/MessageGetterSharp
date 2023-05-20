using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    internal class TextHelper
    {
        public static string HTMLToText(string html)
        {
            string ret = string.Empty;

            // Create an HtmlDocument object
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            foreach (HtmlNode node in htmlDoc.DocumentNode.Descendants())
            {
                switch (node.NodeType)
                {
                    case HtmlNodeType.Text:
                        string text = node.InnerText;

                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            ret += text;
                        }
                        break;

                    case HtmlNodeType.Element:
                        switch (node.Name)
                        {
                            case "br":
                                ret += "\n";
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
            return ret;
        }
    }
}
