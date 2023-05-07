using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class Message : IComparable<Message>
    {
        public User User { get; set; }

        public string ID { get; set; } //消息唯一识别号，由来源和ID构成
        public DateTime CreateAt { get; set; } //发布时间
        public bool IsTop { get; set; } = false; //是否是该用户的置顶
        public Message? Repost { get; set; } //转发自
        public string Text { get; set; } //消息正文

        /// <summary>
        /// 消息是否已经加载
        /// </summary>
        public bool Inited { get; set; } = false;
        public List<Media> Medias { get; set; } = new List<Media>(); //包含的媒体
        public object Tag { get; set; }

        /// <summary>
        /// 重写的CompareTo方法，时间排序
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Message other)
        {
            if (null == other)
            {
                return 1;
            }
            return other.CreateAt.CompareTo(this.CreateAt);//降序
        }

        public virtual void Init()
        {
            if(Inited) { return; }
        }
    }
}
