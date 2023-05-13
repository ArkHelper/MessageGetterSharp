using MessageGetter.Medias;
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

        public delegate void NewMessageAddedHandler(Message message, MessageInfo messageInfo);

        /// <summary>
        /// 新消息入库
        /// </summary>
        public static event NewMessageAddedHandler NewMessageAdded;

        public static Dictionary<Message, MessageInfo> Container = new();
        internal static Dictionary<User, UserInfo> Users = new();

        public static Configuration Configuration { get; set; }
        private static Thread? Interval;
        static Getter()
        {
            Configuration = new Configuration();
            Configuration.PropertyChanged += Configuration_PropertyChanged;
        }

        internal static async Task NewMessageFromFresh(Message message, MessageInfo messageInfo)
        {
            try
            {
                /* 查询消息表中是否有该条消息
                 * 有就改变它的MessageInfo
                 * （有些repost引用时MessageCreatedBy会初始化为repost，如果这条消息又被列表里的用户刷新那么就要改变来源）
                 */
                Container[Container.First(t => t.Key.ID == message.ID).Key] = messageInfo;
            }
            catch //表中没有该消息：
            {
                message.Init();//构建

                initMedia(message.Medias);//下载图片和视频

                //处理转发消息
                var repost = message.Repost;
                if (repost != null)
                {
                    repost.Init();//构建转发消息
                    initMedia(repost.Medias);

                    //初始化转发消息作者
                    if (!repost.User.ProfileInited)
                    {
                        await repost.User.InitProfile();
                        Users.Add(repost.User, new UserInfo() { UserCreatedBy = CreatedByType.repost });
                    }
                }

                void initMedia(List<Media> medias)
                {
                    foreach (Media media in medias)
                    {
                        if (media.GetType() == typeof(Video) && Getter.Configuration.AutoDownloadVideo)
                            media.Download();
                        if (media.GetType() == typeof(Picture) && Getter.Configuration.AutoDownloadPicture)
                            media.Download();
                    }
                }

                //筛选
                if (Configuration.Filter == null || Configuration.Filter(message))
                {
                    Container.Add(message, messageInfo);//加入表中
                    NewMessageAdded?.Invoke(message, messageInfo);//事件提醒
                }
            }
        }

        public static void AddUser(User user)
        {
            Users.Add(user, new UserInfo() { UserCreatedBy = CreatedByType.user });
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

        private static async void Fresh()
        {
            foreach (var user in
                Getter.Users.Where(x => x.Value.UserCreatedBy == CreatedByType.user).Select(x => x.Key).ToList())
            {
                await user.UpdateMessage();
            }
            MessageFreshed?.Invoke("MessageGetterSharp", new EventArgs());
        }
    }
}
