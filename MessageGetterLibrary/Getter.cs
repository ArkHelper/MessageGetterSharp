using MessageGetter.Users.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class Getter<dictValueKind>
    {
        /// <summary>
        /// 消息刷新事件
        /// </summary>
        public static event EventHandler MessageFreshed;
        /// <summary>
        /// 新消息入库
        /// </summary>
        public static event EventHandler<Message> NewMessageHaveAdded;

        private Dictionary<Message, dictValueKind> Container;
        private Thread? Interval;

        public List<Users.User> Users { get; set; }
        public Configuration Configuration { get; set; }

        public Getter(Configuration configuration)
        {
            Configuration = configuration;
        }

        public void SetContainer(Dictionary<Message,dictValueKind> containerAsDict)
        {
            Container = containerAsDict;
        }

        public void StartInterval()
        {
            if (Container == null)
            {
                throw new InvalidOperationException("消息装载容器未指定。");
            }
            BuildInterval();
            Interval.Start();
        }

        public void StopInterval()
        {
            Interval?.Abort();
            Interval = null;
        }

        public async void ForceFresh(Action<bool> callback)
        {
            await Task.Run(() =>
            {
                Fresh();
            });
            callback(true);
        }

        private void BuildInterval()
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

        private void Fresh()
        {
            MessageFreshed?.Invoke(this,null);
            //TODO:
        }

    }
}
