using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class Weibo : User
    {
        private string _uid;
        public string UID
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public Weibo(string uid)
        {
            _uid = uid;
            ID = "weibo" + uid;
        }

        internal override async Task UpdateInfo()
        {
            WebAPI.Weibo.UserProfile(this);
            ProfileInited = true;
        }

        internal override async Task UpdateMessage(MessageUpdateConfiguration? messageUpdateConfiguration)
        {
            var weibos = WebAPI.Weibo.Page(UID, 1);
            foreach (var weibo in weibos.EnumerateArray())
            {
                bool _top = false;
                if (weibo.GetProperty("profile_type_id").GetString().Contains("top")) { _top = true; }
                var json = weibo.GetProperty("mblog");

                var message = new WeiboMessage(json)
                {
                    IsTop = _top,
                    User = this
                };
                message.Init();

                if (message.Repost != null)
                {
                    message.Repost.Init();

                    

                }
                // TODO: user

                if (!Getter.Container.Contains(message))
                {
                    Getter.Container.Add(message);
                }
            }
        }
    }
}
