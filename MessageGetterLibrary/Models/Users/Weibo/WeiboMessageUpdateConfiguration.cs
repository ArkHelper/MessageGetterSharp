using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    internal class WeiboMessageUpdateConfiguration : MessageUpdateConfiguration
    {
        private int _page;
        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
            }
        }
        public WeiboMessageUpdateConfiguration()
        {
            _page = 0;
        }
    }
}
