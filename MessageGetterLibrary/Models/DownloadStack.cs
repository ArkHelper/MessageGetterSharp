using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    internal class DownloadStack:Stack<FileFromNetworkOrLocal>
    {
    }
}
