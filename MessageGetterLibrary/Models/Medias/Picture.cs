using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGetter.Medias
{
    public class Picture : Media
    {
        public Picture? View { get; set; }
        public Picture(string id) : base(id)
        {
        }

        public event EventHandler<AsyncCompletedEventArgs>? CreateViewCompleted;
        public async void CreateView()
        {
            // TODO:createViewImpl(local)
        }
    }
}
