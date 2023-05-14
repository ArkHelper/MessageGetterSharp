using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDemo.Control
{
    /// <summary>
    /// PictureViewer.xaml 的交互逻辑
    /// </summary>
    public partial class PictureViewer : UserControl
    {
        private Geometry _clip;
        public Geometry Clip
        {
            get
            {
                return _clip;
            }
            set
            {
                _clip = value;

                var _ = SearchImageUI(rootGrid);
                if (_ != null)
                {
                    _.Clip = value;
                }
            }
        }
        public Picture Picture { get; set; }

        public PictureViewer(Picture picture)
        {
            InitializeComponent();

            Picture = picture;

            if (picture.Local != null)
            {
                InitImage();
            }
            else
            {
                picture.DownloadProgressChanged += (s, e) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (PgB.IsIndeterminate)
                        {
                            PgB.IsIndeterminate = false;
                        }
                        PgB.Value = e.ProgressPercentage;
                    });
                };

                picture.DownloadCompleted += (s, e) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        InitImage();
                    });
                };
            }
        }

        private void InitImage()
        {
            var userAvatarBitmapImage = RuntimeStorageHelper.PictureStorage.Get(Picture);
            Image userAvatarImageUI = new Image()
            {
                Name = "Image",
                Height = this.Height,
                Width = this.Width,
                VerticalAlignment = VerticalAlignment.Center,
                /*Tag = user,*/
                Source = userAvatarBitmapImage,
                Clip = _clip,
            };
            rootGrid.Children.Clear();
            rootGrid.Children.Add(userAvatarImageUI);
        }

        private static Image? SearchImageUI(Grid fatherGrid)
        {
            foreach (var item in fatherGrid.Children)
            {
                if (item is Image)
                {
                    return (Image)item;
                }
            }
            return null;
        }

        private void rootElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Picture.Local,
                    UseShellExecute = true,
                });
            }
            catch { }
        }
    }
}
