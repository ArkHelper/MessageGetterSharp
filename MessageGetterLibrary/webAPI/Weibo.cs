using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using RestSharp;
using static MessageGetter.WebAPI.Net;

namespace MessageGetter.WebAPI
{
    internal static class Weibo
    {
        private readonly static string mHOST = "https://m.weibo.cn";
        private readonly static string HOST = "https://weibo.com";
        
        public static void UserProfile(MessageGetter.Weibo user)
        {
            string URL = mHOST + "/api/container/getIndex?type=uid&value=" + user.UID;

            JsonElement _userinfo;

            for (int i = 0; i < TimesOut; i++)
            {
                try
                {
                    using (var client = new RestClient(URL))
                    {
                        _userinfo = JsonSerializer.Deserialize<JsonElement>(client.Get(GetRequest).Content).GetProperty("data").GetProperty("userInfo");
                    }
                    user.Avatar = new Medias.Picture(user.ID + "avatar") { Link = _userinfo.GetProperty("profile_image_url").GetString() };
                    user.Name = _userinfo.GetProperty("screen_name").GetString();
                    return;
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
            throw new Exception();
        }

        /*public static Message Message(string message)
        {
            // TODO: IMPL
            return new Message();
        }*/

        public static JsonElement Page(string uid,int page)
        {
            string URL = mHOST + @"/api/container/getIndex?type=uid&value=" + uid + @"&containerid=107603" + uid + "&page=" + page;
            JsonElement _getresult;
        
            for (int i = 0; i < TimesOut; i++)
            {
                try
                {
                    using (var client = new RestClient(URL))
                    {
                        _getresult = JsonSerializer.Deserialize<JsonElement>(client.Get(GetRequest).Content);
                        if (_getresult.GetProperty("ok").GetInt16() != 1) throw new Exception();
                        var weibos = _getresult.GetProperty("data").GetProperty("cards");
                        return weibos;
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
            throw new Exception();
        }

        public static JsonElement Detail(string id)
        {
            string URL = mHOST + "/detail/" + id;
            string response;
        start:;
            try
            {
                var client = new RestClient(URL);

                response = client.Get(GetRequest).Content;
                int _a = response.IndexOf(@"""status""");
                int _b = response.IndexOf(@"""call"":");
                response = response.Substring(_a, _b - _a);
                int _c = response.IndexOf(@"""status""");
                response = response.Substring(_c, response.LastIndexOf(",") - _c);
                response = "{" + response + "}";
                client.Dispose();
            }
            catch
            {
                goto start;
            }
            return JsonSerializer.Deserialize<JsonElement>(response).GetProperty("status");
        }
    }
}
