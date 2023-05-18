using MessageGetter.Medias;
using MessageGetter.Storage;
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
using static MessageGetter.Storage.Container;

namespace MessageGetter
{
    public static class Getter
    {
        #region API

        /// <summary>
        /// 新消息入库委托
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageInfo"></param>
        public delegate void NewMessageAddedHandler(Message message, MessageInfo messageInfo);
        /// <summary>
        /// 新消息入库
        /// </summary>
        public static event NewMessageAddedHandler NewMessageAdded;

        /// <summary>
        /// 轮询刷新结束
        /// </summary>
        public static event EventHandler<int> PollingFreshEnded;

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        public static void AddUser(User user)
        {
            UsersContainer.Add(user, new UserInfo() { UserCreatedBy = Status.createdBy_user });
        }
        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="id">用户ID，例如<see cref="string">weibo6279793937</see></param>
        /// <exception cref="NullReferenceException"></exception>
        public static void RemoveUser(string id)
        {
            User user;
            try
            {
                user = UsersContainer.First(t => t.Key.ID == id).Key;
                UsersContainer.Remove(user);
            }
            catch
            {
                throw new NullReferenceException();
            }

        }

        /// <summary>
        /// 配置文件
        /// </summary>
        public static Configuration Configuration { get; set; }

        #endregion

        #region API_控制
        /// <summary>
        /// 开始消息获取
        /// </summary>
        public static void Start()
        {
            MakeThread();
            Thread.Start();
        }

        /// <summary>
        /// 停止消息获取
        /// </summary>
        public static void Stop()
        {
            Thread.Abort();
        }

        /*private static async void ForceFresh()
        {
            // TODO:IMPL
            Task task = new Task(() =>
            {
                Fresh();
            });
            task.Start();
            await task;
            callback?.Invoke(true);
        }*/
        #endregion

        private static int _pollingFreshTimes = 0;
        private static void MakeThread()
        {
            Thread = new Thread(async () =>
            {
                while (true)
                {
                    foreach (var user in UsersContainer.Keys.ToList())
                    {
                        await user.UpdateMessage();
                    }
                    _pollingFreshTimes++;
                    PollingFreshEnded?.Invoke("MessageGetterSharp",_pollingFreshTimes);
                    Thread.Sleep(Configuration.Interval);
                }

            });
        }
        private static Thread Thread;

        static Getter()
        {
            Configuration = new Configuration(); // 设定默认配置
            Configuration.PropertyChanged += Configuration_PropertyChanged; // 配置改变事件
        }

        /// <summary>
        /// 配置改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Configuration_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// 消息入库校验&通知
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="messageInfo">消息状态</param>
        /// <returns></returns>
        internal static async Task NewMessage(Message message, MessageInfo messageInfo)
        {
            var operateTargetIfExist = MessageContainer.FirstOrDefault(t => t.Key.ID == message.ID); // 查询消息表中是否有该条消息

            if (operateTargetIfExist.Key != null)
            {
                MessageContainer[operateTargetIfExist.Key] = messageInfo; // 有就改变它的MessageInfo（有些repost引用时MessageCreatedBy会初始化为repost，如果这条消息又被列表里的用户刷新那么就要改变来源）
            }
            else //表中没有该消息
            {
                initMessage(message);

                void initMessage(Message messageToinit)
                {
                    if (messageToinit == null) return;

                    messageToinit.Init();
                    initUser(messageToinit.User);
                    initMessage(messageToinit.Repost);
                    initMediaList(messageToinit.Medias);
                }
                void initUser(User user)
                {
                    if (!user.ProfileInited)
                    {
                        var _ = user.InitProfile();
                        _.Wait();
                    }
                        
                    if (!UsersContainer.TryGetValue(user, out var userinfo))
                    {
                        UsersContainer.Add(user, new UserInfo { UserCreatedBy = Status.createdBy_repost });
                    }
                    initMedia(user.Avatar);
                }
                void initMediaList(List<Media> medias)
                {
                    medias.ForEach(t=>initMedia(t));
                }
                void initMedia(Media? media)
                {
                    if (media == null) return;
                    if (media.GetType() == typeof(Video) && Getter.Configuration.AutoDownloadVideo)
                    {
                        media.Download();
                        initMedia((media as Video).Cover);
                    }
                    if (media.GetType() == typeof(Picture) && Getter.Configuration.AutoDownloadPicture)
                        media.Download();
                }

                MessageContainer.Add(message, messageInfo);
                //筛选
                if (Configuration.Filter == null || Configuration.Filter(message))
                {
                    messageInfo.MessageStatus = Status.createdBy_fresh_hide;
                    NewMessageAdded?.Invoke(message, messageInfo);//事件提醒
                }
            }
        }
    }
}