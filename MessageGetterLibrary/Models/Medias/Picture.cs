using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Medias
{
    public class Picture : Media
    {
        public Picture(string id) : base(id)
        {
        }

        public async Task Download()
        {
            base.DownloadCompleted += async (s, e) =>
            {
                await CreateView();
                this.DownloadCompleted?.Invoke(this, e);
            };
            await base.Download();
        }

        public override event EventHandler<AsyncCompletedEventArgs>? DownloadCompleted;

        private int ViewSize = 200;

        //public event EventHandler<AsyncCompletedEventArgs>? CreateViewCompleted;
        protected override async Task CreateView()
        {
            if (View != null && View.Local == null && View.Link != null)
            {
                await View.Download();
                return;
            }

            string outid = ID + "_small";
            string outLocal = DirHelper.Media + "\\" + outid;
            this.View = new Picture(outid);

            if (View.Local == null)
            {
                using (Bitmap input = new Bitmap(this.Local))
                using (Bitmap output = new Bitmap(ViewSize, ViewSize))
                {
                    int a = input.Width > input.Height ? input.Height : input.Width;
                    var area = input.Width > input.Height ?
                        new Rectangle((input.Width - a) / 2, 0, a, a)
                        : new Rectangle(0, (input.Height - a) / 2, a, a);
                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.DrawImage(input, new Rectangle(0, 0, ViewSize, ViewSize),
                                    area,
                                    GraphicsUnit.Pixel);
                    };
                    output.Save(outLocal);
                    View.Local = outLocal;
                }
                GC.Collect();
            }
        }
    }
}
