using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Medias
{
    public class Video:Media
    {
        public string CoverLink { get; set; }
        public Video(string id):base(id) { }

        protected override async Task CreateView()
        {
            if (View != null)
            {
                await View.Download();
                return;
            }
        }
    }
}
