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

        private bool _small = true;
        public bool Small
        {
            get { return _small; }
            set
            {
                _small = value;
            }
        }

        public string? Link { get; set; } = null;

        public PictureViewer(Picture picture,bool small)
        {
            InitializeComponent();

            Picture = picture;
            Small = small;

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
            try
            {
                var bitmapImage = Helper.PictureStorageHelper.Get(Picture.Local, false);

                if (bitmapImage.Width < bitmapImage.Height) _small = true;
                if (_small)
                {
                    bitmapImage = Helper.PictureStorageHelper.Get(Picture.Local, true);
                }

                Image ImageUI = new Image()
                {
                    Name = "Image",
                    VerticalAlignment = VerticalAlignment.Center,
                    /*Tag = user,*/
                    Source = bitmapImage,
                    Clip = _clip,
                };
                if (_small)
                {
                    this.Width = 100;
                    this.Height = 100;
                }
                else
                {
                    this.Width = double.NaN;
                    this.Height = double.NaN;
                }
                rootGrid.Children.Clear();
                rootGrid.Children.Add(ImageUI);
            }
            catch (Exception ex)
            {
                InitExpection();
                Console.WriteLine(ex.ToString());
            }
        }
        private void InitExpection()
        {
            PgB.Visibility = Visibility.Collapsed;
            ExpectionIcon.Visibility = Visibility.Visible;
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
                    FileName = (Link != null)?Link:Picture.Local,
                    UseShellExecute = true,
                });
            }
            catch { }
        }
    }
}
