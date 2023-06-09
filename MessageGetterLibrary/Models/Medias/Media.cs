﻿using Downloader;
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

        public object? Tag { get; set; }

        public Media(string id)
        {
            ID = id;

            var idForLocal = DirHelper.PraseStringToFileName(ID);

            ExpectedLocal = Path.Combine(DirHelper.Media, idForLocal);
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
