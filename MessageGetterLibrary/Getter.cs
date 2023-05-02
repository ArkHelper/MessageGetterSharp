using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MessageGetter
{
    public static class Getter
    {
        /// <summary>
        /// 消息刷新事件
        /// </summary>
        public static event EventHandler? MessageFreshed;

        public delegate void NewMessageAddedHandler(Message message,MessageInfo messageInfo);

        /// <summary>
        /// 新消息入库
        /// </summary>
        public static event NewMessageAddedHandler NewMessageAdded;

        public static Dictionary<Message, MessageInfo> Container;
        internal static Dictionary<User, UserInfo> Users;

        public static Configuration Configuration { get; set; }
        private static Thread? Interval;
        static Getter()
        {
            Configuration = new Configuration();
            Configuration.PropertyChanged += Configuration_PropertyChanged;
        }

        internal static void AddNewMessage(Message message, MessageInfo messageInfo)
        {
            Container.Add(message, messageInfo);
            NewMessageAdded?.Invoke(message, messageInfo);
        }

        public static void AddUser(User user)
        {
            Users.Add(user,new UserInfo() { UserCreatedBy = CreatedByType.user});
        }
        public static void RemoveUser(string id)
        {
            User user;
            try
            {
                user = Users.First(t => t.Key.ID == id).Key;
                Users.Remove(user);
            }
            catch
            {
                throw new NullReferenceException();
            }
            
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
