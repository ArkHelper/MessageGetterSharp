using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Medias
{
    internal class Video:Media
    {
        public Picture? View { get; set; }
        public Video(string id):base(id) { }
    }
}
