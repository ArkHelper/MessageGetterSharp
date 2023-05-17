using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Helper
{
    internal class ImageHelper
    {
        public static bool CheckImageIntegrity(string imagePath)
        {
            try
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        byte[] buffer = new byte[4];
                        binaryReader.Read(buffer, 0, buffer.Length);
                        return IsJPEG(buffer) || IsPNG(buffer); // 判断是否为 JPEG 或 PNG 图片格式
                    }
                }
            }
            catch
            {
                return false; // 图片不完整
            }
        }

        private static bool IsJPEG(byte[] buffer)
        {
            return buffer.Length >= 2 && buffer[0] == 0xFF && buffer[1] == 0xD8; // JPEG 文件头标识
        }

        private static bool IsPNG(byte[] buffer)
        {
            return buffer.Length >= 8 && buffer[0] == 0x89 && buffer[1] == 0x50 && buffer[2] == 0x4E && buffer[3] == 0x47 && buffer[4] == 0x0D && buffer[5] == 0x0A && buffer[6] == 0x1A && buffer[7] == 0x0A; // PNG 文件头标识
        }
    }
}

