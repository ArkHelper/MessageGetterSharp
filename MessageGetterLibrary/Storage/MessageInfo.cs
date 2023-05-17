using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageGetter.Storage;

namespace MessageGetter
{
    public class MessageInfo
    {
        public MessageInfo() { }
        
        public Status MessageStatus { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            if (obj.GetType() != typeof(MessageInfo)) return false;

            var msgifo = obj as MessageInfo;
            if (msgifo.MessageStatus != this.MessageStatus) return false;

            return true;
        }
    }
}
