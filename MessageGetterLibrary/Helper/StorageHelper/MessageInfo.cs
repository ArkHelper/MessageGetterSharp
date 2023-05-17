using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class MessageInfo
    {
        public MessageInfo() { }
        
        public CreatedByType MessageCreatedBy { get; set; }
        public bool Hide { get; set; } = false;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            if (obj.GetType() != typeof(MessageInfo)) return false;

            var msgifo = obj as MessageInfo;
            if (msgifo.MessageCreatedBy != this.MessageCreatedBy) return false;
            if (msgifo.Hide != this.Hide) return false;

            return true;
        }
    }
}
