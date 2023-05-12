using MessageGetter;
using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class User
    {
        private string id;
        /// <summary>
        /// MessageGetter用于唯一识别的ID，由子类实现
        /// </summary>
        public string ID
        {
            get { return id; }
            protected set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string nickname;
        public string NickName
        {
            get { return nickname; }
            set
            {
                if (nickname != value)
                {
                    nickname = value;
                    OnPropertyChanged(nameof(NickName));
                }
            }
        }

        private Picture avatar;
        public Picture Avatar
        {
            get { return avatar; }
            set
            {
                if (avatar != value)
                {
                    avatar = value;
                    OnPropertyChanged(nameof(Avatar));
                }
            }
        }

        private bool profileInited;
        /// <summary>
        /// 用户资料加载状态
        /// </summary>
        public bool ProfileInited
        {
            get { return profileInited; }
            set
            {
                if (profileInited != value)
                {
                    profileInited = value;
                    OnPropertyChanged(nameof(ProfileInited));
                }
            }
        }

        public User()
        {
            ID = string.Empty;
            Name = string.Empty;
            NickName = string.Empty;
            Avatar = null;
            ProfileInited = false;
        }

        internal virtual async Task InitProfile()
        {
            throw new NotImplementedException();
        }

        internal virtual async Task UpdateMessage(MessageUpdateConfiguration? messageUpdateConfiguration = null)
        {
            if(!ProfileInited) InitProfile();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
