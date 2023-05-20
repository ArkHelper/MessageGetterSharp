using MessageGetter.Medias;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class WeiboMessage : Message, INotifyPropertyChanged
    {
        /// <summary>
        /// 微博原始文本（HTML）
        /// </summary>
        private string rawText;
        public string RawText
        {
            get { return rawText; }
            set
            {
                if (rawText != value)
                {
                    rawText = value;
                    OnPropertyChanged(nameof(RawText));
                }
            }
        }

        // 原始Json
        private JsonElement Json;

        public WeiboMessage(JsonElement json)
        {
            Json = json;
        }

        public override void Init()
        {
            base.Init();

            //微博的longtext解析
            if (Json.GetProperty("isLongText").GetBoolean()//||true
                )
            {
                Json = WebAPI.Weibo.Detail(Json.GetProperty("id").GetString());
            }

            //User
            if (User == null)
            {
                var userID = Json.GetProperty("user").GetProperty("id").GetDouble().ToString();
                try
                {
                    User = Storage.Container.UsersContainer.First(t => t.Key.ID == "weibo" + userID).Key;
                }
                catch
                {
                    User = new Weibo(userID);
                }
            }

            //解析时间
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            CreateAt = DateTime.ParseExact(Json.GetProperty("created_at").GetString(), "ddd MMM d HH:mm:ss zz00 yyyy", cultureInfo);

            //获取文字并从HTML反转义
            RawText = System.Net.WebUtility.HtmlDecode(Json.GetProperty("text").GetString());
            Text = RawText;

            //文字处理（html转段落）//future解决误杀正常文字
            while (Text.Contains('<') && Text.Contains('>'))
            {
                Text = Text.Replace("<br />", "\n");
                int _lef = Text.IndexOf("<");
                if (_lef != -1)
                    Text = Text.Remove(_lef, Text.IndexOf(">") - _lef + 1); //换行
            }

            //转发
            if (Json.TryGetProperty("retweeted_status", out var _ret))
            {
                var repostID = _ret.GetProperty("id").GetString();

                try
                {
                    Repost = Storage.Container.MessageContainer.First(t => t.Key.ID == "weibo" + repostID).Key;
                }
                catch
                {
                    Repost = new WeiboMessage(_ret);
                }
            }

            if (Json.TryGetProperty("region_name",out var _region))
            {
                Region = _region.GetString();
            }

            //图片/视频获取
            if (Json.TryGetProperty("pics", out var _pics))
            {
                var pics = _pics.EnumerateArray();
                foreach (var item in pics)
                {
                    var _large = item.GetProperty("large");
                    /*var _size = _large.GetProperty("geo");
                    int _hei = 0;
                    int _wid = 0;
                    var hej = _size.GetProperty("height");
                    var wij = _size.GetProperty("width");
                    try
                    {
                        _hei = Convert.ToInt32(hej.GetString());
                        _wid = Convert.ToInt32(wij.GetString());
                    }
                    catch
                    {
                        _hei = hej.GetInt32();
                        _wid = wij.GetInt32();
                    }*/
                    string url = _large.GetProperty("url").GetString();
                    string id = item.GetProperty("pid").GetString();
                    Medias.Add(new Picture(id) { Link = url });
                }
            }
            else
            {
                if (Json.TryGetProperty("page_info", out var pageInfo) && Repost == null)
                {
                    if (pageInfo.TryGetProperty("media_info", out var mediaInfo))
                    {
                        Video video = new(pageInfo.GetProperty("object_id").GetString());
                        if (pageInfo.TryGetProperty("page_pic", out var pagePic))
                        {
                            if (pagePic.TryGetProperty("url", out var _url))
                            {
                                string url = _url.GetString();
                                video.Cover = new Picture(pagePic.GetProperty("pid").GetString()) { Link = url };
                            }
                        }
                        if (mediaInfo.TryGetProperty("stream_url_hd", out var streamUrl))
                        {
                            video.Link = pageInfo.GetProperty("page_url").GetString();
                        }
                        Medias.Add(video);
                    }
                }
            }

            Inited = true;
        }
    }
}
