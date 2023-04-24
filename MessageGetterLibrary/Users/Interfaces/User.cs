using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Users
{
    public class User
    {
        /// <summary>
        /// MessageGetter用于唯一识别的ID，由平台号+平台ID组成
        /// <example>
        /// <br></br>
        /// 明日方舟 在 微博 的ID为6279793937，那么其ID为 weibo6279793937
        /// </example>
        /// </summary>
        public string ID { get; set; }

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
/*
        /// <summary>
        /// 用户资料更新状态
        /// </summary>
        public int ProfileInitStatus { get; set; }*/

        public User()
        {
            ID = string.Empty;
            Name = string.Empty;
            NickName = string.Empty;
            Avatar = string.Empty;
            Tag = null;
            MessageUpdateStatus = 0;
            /*ProfileInitStatus = 0;*/
        }
    }
}
