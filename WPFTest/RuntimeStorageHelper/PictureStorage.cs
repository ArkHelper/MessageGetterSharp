using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPFDemo.RuntimeStorageHelper
{
    public static class PictureStorage
    {
        private static List<BitmapImage> _images = new List<BitmapImage>();
        public static BitmapImage Get(Picture picture)
        {
            var @return = _images.Find(t => t.UriSource.AbsoluteUri == picture.Local);
            if (@return == null)
            {
                @return = new BitmapImage(new Uri(picture.Local));
                @return.Freeze();
                _images.Add(@return);
            }
            return @return;
        }

        public static void Clear()
        {
            _images.Clear();
        }
    }
}
