using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    internal static class DownloadStack
    {
        private static Stack<FileFromNetworkOrLocal> _stack;

        public static void Start()
        {
            
        }

        public static void Stop()
        {

        }

        public static void Pause()
        {

        }

        public static void Resume()
        {

        }

        public static void Add(FileFromNetworkOrLocal file)
        {
            _stack.Push(file);
        }
    }
}
