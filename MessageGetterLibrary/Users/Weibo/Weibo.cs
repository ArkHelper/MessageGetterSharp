using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageGetter.Users.Interfaces;

namespace MessageGetter.Users
{
    public class Weibo : User, IMessageUpdateAble, IUpdateInfoAble
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

        public void UpdateInfo()
        {
            webAPI.Weibo.UserProfile(this);
        }

        public void UpdateMessage(MessageUpdateConfiguration? messageUpdateConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
