using MessageGetter.Medias;
using MessageGetter;
using System;
using System.Drawing;
using System.IO;

namespace WPFDemo.Helper
{
    internal class PictureViewCreator
    {
        public static void Create(Picture picture)
        {
            int viewSize = 100;
            string outid = picture.ID + "_small";
            string outLocal = DirHelper.Media + "\\" + outid + ".jpg";

            if (File.Exists(outLocal))
            {
                using (Bitmap input = new Bitmap(picture.Local))
                using (Bitmap output = new Bitmap(viewSize, viewSize))
                {
                    int a = input.Width > input.Height ? input.Height : input.Width;
                    var area = input.Width > input.Height ?
                        new Rectangle((input.Width - a) / 2, 0, a, a)
                        : new Rectangle(0, (input.Height - a) / 2, a, a);
                    using (Graphics g = Graphics.FromImage(output))
                    {
                        g.DrawImage(input, new Rectangle(0, 0, viewSize, viewSize),
                                    area,
                                    GraphicsUnit.Pixel);
                    };
                    output.Save(outLocal);
                }
                GC.Collect();
            }
        }
    }
}
