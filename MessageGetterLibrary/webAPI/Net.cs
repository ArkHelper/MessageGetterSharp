using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.webAPI
{
    internal class Net
    {
        internal readonly static RestRequest GetRequest = new RestRequest() { Method = Method.Get };
        internal readonly static int TimesOut = 3;
    }
}
