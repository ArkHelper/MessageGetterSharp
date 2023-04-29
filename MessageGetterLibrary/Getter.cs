using MessageGetter.Users;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public static class Getter
    {
        /// <summary>
        /// 消息刷新事件
        /// </summary>
        public static event EventHandler? MessageFreshed;
        /// <summary>
        /// 新消息入库
        /// </summary>
        public static event EventHandler<Message>? NewMessageHaveAdded;

        private static List<Message>? Container;
        private static Thread? Interval;

        public static List<User> Users { get; set; } = new List<User>();
        public static Configuration Configuration { get; set; }

        static Getter()
        {
            Configuration = new Configuration();
            Configuration.PropertyChanged += Configuration_PropertyChanged;
        }

        private static void Configuration_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {                
        }

        public static void StartInterval()
        {
            BuildInterval();
            Interval.Start();
        }

        public static void StopInterval()
        {
            Interval?.Abort();
            Interval = null;
        }

        public static async void ForceFresh(Action<bool>? callback = null)
        {
            Task task = new Task(() =>
            {
                Fresh();
            });
            task.Start();
            await task;
            callback?.Invoke(true);
        }

        private static void BuildInterval()
        {
            Interval = new Thread(() =>
            {
                while (true)
                {
                    Fresh();
                    Thread.Sleep(Configuration.Interval);
                }
            });
        }

        private static void Fresh()
        {
            //start
            Thread.Sleep(5000);
            //end
            MessageFreshed?.Invoke("MessageGetterSharp", new EventArgs());
        }
    }
}
