using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPFDemo.Helper
{
    internal static class PictureStorageHelper
    {
        private static List<BitmapImage> _images = new List<BitmapImage>();
        public static BitmapImage Get(string path,bool small)
        {
            FileInfo fileInfo = new FileInfo(path);
            string dic = fileInfo.DirectoryName;
            string filename = ((small)?"small_":"") + fileInfo.Name;
            string fullpath = Path.Combine(dic, filename);

            var @return = _images.Find(t => t.UriSource.AbsoluteUri == fullpath);
            if (@return == null)
            {
                @return = new BitmapImage(new Uri(fullpath));
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
