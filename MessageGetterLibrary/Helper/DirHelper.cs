using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class DirHelper
    {
        private static string Check(string addr)
        {
            Directory.CreateDirectory(addr);
            return addr;
        }
        public static string Root =>
            Check(Getter.Configuration.RootDir);
        public static string Media => Check(Root + @"\file\media");
        public static string Picture_Small => Check(Media + @"\picture_small");
        public static string Picture_Video => Check(Media + @"\picture_video");

        /// <summary>
        /// 处理文件名称
        /// </summary>
        /// <param name="fileNameFormat">文件格式</param>
        /// <returns>返回合法的文件名</returns>
        public static string PraseStringToFileName(string fileNameFormat)
        {
            char[] strs = "+#?*\"<>/;,-:%~".ToCharArray();
            foreach (char c in strs)
                fileNameFormat = fileNameFormat.Replace(c.ToString(), "_");

            strs = "：，。；？".ToCharArray();
            foreach (char c in strs)
                fileNameFormat = fileNameFormat.Replace(c.ToString(), "_");

            //去掉空格.
            while (fileNameFormat.Contains(" ") == true)
                fileNameFormat = fileNameFormat.Replace(" ", "");

            //替换特殊字符.
            fileNameFormat = fileNameFormat.Replace("\t\n", "");

            //处理合法的文件名.
            StringBuilder rBuilder = new StringBuilder(fileNameFormat);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
                rBuilder.Replace(rInvalidChar.ToString(), string.Empty);

            fileNameFormat = rBuilder.ToString();

            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace("__", "_");
            fileNameFormat = fileNameFormat.Replace(" ", "");
            fileNameFormat = fileNameFormat.Replace(" ", "");
            fileNameFormat = fileNameFormat.Replace(" ", "");
            fileNameFormat = fileNameFormat.Replace(" ", "");
            fileNameFormat = fileNameFormat.Replace(" ", "");
            fileNameFormat = fileNameFormat.Replace(" ", "");
            fileNameFormat = fileNameFormat.Replace(" ", "");
            fileNameFormat = fileNameFormat.Replace(" ", "");

            if (fileNameFormat.Length > 240)
                fileNameFormat = fileNameFormat.Substring(0, 240);

            return fileNameFormat;
        }
    }
}
