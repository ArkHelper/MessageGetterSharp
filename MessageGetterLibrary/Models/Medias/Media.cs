using System.ComponentModel;
using System.Diagnostics;

namespace MessageGetter.Medias
{
    public class Media : FileFromNetworkOrLocal
    {
        private string _id;
        public string? ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public Picture? View { get; set; }

        public object? Tag { get; set; }

        public Media(string id)
        {
            ID = id;

            var idForLocal = DirHelper.PraseStringToFileName(ID + ".jpg");

            ExpectedLocal = Path.Combine(DirHelper.Media, idForLocal);
            if (File.Exists(ExpectedLocal))
            {
                if (this is Picture)
                {
                    if (!Helper.ImageHelper.CheckImageIntegrity(ExpectedLocal))
                        File.Delete(ExpectedLocal);
                }
                Local = ExpectedLocal;
            }
        }

        //public override event EventHandler<AsyncCompletedEventArgs>? DownloadCompleted;

        /// <summary>
        /// 下载该图片
        /// </summary>
        /// <param name="evenIfExist">即使本地已经有该ID命名的图片文件</param>
        /// <returns></returns>
        public async Task Download(bool evenIfExist = false)
        {
            base.Download(ExpectedLocal, evenIfExist);
        }

        protected virtual Task CreateView()
        {
            throw new NotImplementedException();
        }

        protected override void OnDownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            Debug.WriteLine(this.ID + "=====completed");
            this.CreateView();
            base.OnDownloadFileCompleted(sender, e);
        }
    }
}
