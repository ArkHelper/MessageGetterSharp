using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageGetter.Users.Interfaces;

namespace MessageGetter.Users
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
            ID = typeof(Weibo).Name + uid;
        }

        public override async Task UpdateInfo()
        {
            WebAPI.Weibo.UserProfile(this);
            ProfileInited = true;
        }

        public override async Task UpdateMessage(MessageUpdateConfiguration? messageUpdateConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
