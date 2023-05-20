using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageGetter.Helper;

namespace MessageGetter.Medias
{
    public class Picture : Media
    {
        public Picture(string id) : base(id)
        {
        }

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
                try
                {
                    Getter.Configuration.ActionAfterPictureDownloaded?.Invoke(this);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                }
                base.OnDownloadFileCompleted(sender, e);
            }
        }
    }
}
