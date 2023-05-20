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
        public Picture Cover { get; set; }
        public Video(string id):base(id) { }

        public async Task Download()
        {
            await base.Download();
        }

        protected override void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {

            }
            else
            {
                Local = ExpectedLocal;
                base.OnDownloadFileCompleted(sender, e);
            }
        }
    }
}
