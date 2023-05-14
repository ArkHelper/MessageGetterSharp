using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Medias
{
    public class Video:Media
    {
        public string CoverLink { get; set; }
        public Video(string id):base(id) { }

        //public override event EventHandler<AsyncCompletedEventArgs>? DownloadCompleted;
        public async Task Download()
        {
            /*base.DownloadCompleted += async (s, e) =>
            {
                //await CreateView();
                this.DownloadCompleted?.Invoke(this, e);
            };*/
            await base.Download();
        }

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
