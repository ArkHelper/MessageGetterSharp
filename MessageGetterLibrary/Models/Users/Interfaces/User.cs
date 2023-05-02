using MessageGetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class User
    {
        /// <summary>
        /// MessageGetter用于唯一识别的ID，由子类实现
        /// </summary>
        public string ID { get; protected set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像对应的图片
        /// </summary>
        public string Avatar { get; set; }

        public object? Tag { get; set; }

        /// <summary>
        /// 消息更新状态，用于指示消息已经更新的次数
        /// </summary>
        public double MessageUpdateStatus { get; set; }

        /// <summary>
        /// 用户资料加载状态
        /// </summary>
        public bool ProfileInited { get; set; }

        public User()
        {
            ID = string.Empty;
            Name = string.Empty;
            NickName = string.Empty;
            Avatar = string.Empty;
            Tag = null;
            MessageUpdateStatus = 0;
            ProfileInited = false;
        }

        internal virtual async Task UpdateInfo()
        {
            throw new NotImplementedException();
        }

        internal virtual async Task UpdateMessage(MessageUpdateConfiguration? messageUpdateConfiguration = null)
        {
            throw new NotImplementedException();
        }
    }
}
