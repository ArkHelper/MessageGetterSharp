using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    internal class DirHelper
    {
        public static string Root => Getter.Configuration.RootDir;
        public static string Media => Root + @"\file\media";
        public static string Picture_Small => Media + @"\picture_small";
        public static string Picture_Video => Media + @"\picture_video";

    }
}
