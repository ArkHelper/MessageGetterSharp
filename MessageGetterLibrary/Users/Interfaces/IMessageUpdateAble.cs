using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Users.Interfaces
{
    internal interface IMessageUpdateAble
    {
        void UpdateMessage(MessageUpdateConfiguration? messageUpdateConfiguration = null);
    }
}
