﻿using MessageGetter.Medias;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter
{
    public class Weibo : User
    {
        private string _uid;
        public string UID
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public Weibo(string uid)
        {
            _uid = uid;
            ID = "weibo" + uid;
        }

        internal override async Task InitProfile()
        {
            WebAPI.Weibo.UserProfile(this);
            ProfileInited = true;
        }

        internal override async Task UpdateMessage(MessageUpdateConfiguration? messageUpdateConfiguration)
        {
            await base.UpdateMessage(messageUpdateConfiguration);
            var weibos = WebAPI.Weibo.Page(UID, 1);
            foreach (var weibo in weibos.EnumerateArray())
            {
                bool _top = false;
                if (weibo.GetProperty("profile_type_id").GetString().Contains("top")) { _top = true; }
                var json = weibo.GetProperty("mblog");
                var id = "weibo" + json.GetProperty("id").GetString();

                WeiboMessage message;
                try
                {
                    message = (WeiboMessage)Getter.Container.First(t => t.Key.ID == id).Key;
                    Getter.Container[message].MessageCreatedBy = CreatedByType.fresh;
                }
                catch
                {
                    message = new WeiboMessage(json)
                    {
                        IsTop = _top,
                        User = this,
                        ID = id
                    };
                    message.Init();
                    initMedia(message.Medias);

                    if (message.Repost != null)
                    {
                        message.Repost.Init();
                        initMedia(message.Repost.Medias);
                        Getter.AddNewMessage(message.Repost, new MessageInfo() { MessageCreatedBy = CreatedByType.repost });
                        if (message.Repost.User.ProfileInited)
                        {
                            await message.Repost.User.InitProfile();
                        }
                        try
                        {
                            Getter.Users.First(t => t.Key == message.Repost.User);
                        }
                        catch
                        {
                            Getter.Users.Add(message.Repost.User, new UserInfo() { UserCreatedBy = CreatedByType.repost });
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

                Getter.AddNewMessage(message, new MessageInfo() { MessageCreatedBy = CreatedByType.fresh });
            }


        }
    }
}
}
