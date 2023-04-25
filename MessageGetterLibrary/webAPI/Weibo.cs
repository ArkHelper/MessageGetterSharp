using MessageGetter.Users;
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
    public static class Weibo
    {
        private readonly static string mHOST = "https://m.weibo.cn";
        private readonly static string HOST = "https://weibo.com";
        
        public static void UserProfile(Users.Weibo user)
        {
            string URL = mHOST + "/api/container/getIndex?type=uid&value=" + user.UID;

            JsonElement _userinfo;

            for (int i = 0; i < TimesOut; i++)
            {
                try
                {
                    using (var client = new RestClient())
                    {
                        _userinfo = JsonSerializer.Deserialize<JsonElement>(client.Get(GetRequest).Content).GetProperty("data").GetProperty("userInfo");
                    }
                    user.Avatar = _userinfo.GetProperty("profile_image_url").GetString();
                    user.Name = _userinfo.GetProperty("screen_name").GetString();
                    return;
                }
                catch
                {
                    continue;
                }
            }
            throw new Exception();
        }

        
    }
}
