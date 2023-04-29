using Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Medias
{
    public class Media:FileFromNetworkOrLocal
    {
        public string? ID { get; set; }

        public object? Tag { get; set; }

        public Media(string id)
        {
            ID = id;

            ExpectedLocal = Path.Combine(DirHelper.Media, ID);
            if (File.Exists(ExpectedLocal))
            {
                Local = ExpectedLocal;
            }
        }

        /// <summary>
        /// 下载该图片
        /// </summary>
        /// <param name="evenIfExist">即使本地已经有该ID命名的图片文件</param>
        /// <returns></returns>
        public async Task Download(bool evenIfExist = false)
        {
            await base.Download(ExpectedLocal, evenIfExist);
        }
    }
}
