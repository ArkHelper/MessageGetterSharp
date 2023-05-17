using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Storage
{
    internal static class Container
    {
        public static Dictionary<Message, MessageInfo> MessageContainer = new();
        internal static Dictionary<User, UserInfo> UsersContainer = new();
        static Container()
        {

        }
    }
}
