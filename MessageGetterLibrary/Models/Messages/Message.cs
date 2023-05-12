using MessageGetter.Medias;
using MessageGetter;
using System.ComponentModel;

public class Message : IComparable<Message>, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private User user;
    public User User
    {
        get { return user; }
        set
        {
            if (user != value)
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
    }

    private string id;
    public string ID
    {
        get { return id; }
        set
        {
            if (id != value)
            {
                id = value;
                OnPropertyChanged(nameof(ID));
            }
        }
    }

    private DateTime createAt;
    public DateTime CreateAt
    {
        get { return createAt; }
        set
        {
            if (createAt != value)
            {
                createAt = value;
                OnPropertyChanged(nameof(CreateAt));
            }
        }
    }

    private bool isTop = false;
    public bool IsTop
    {
        get { return isTop; }
        set
        {
            if (isTop != value)
            {
                isTop = value;
                OnPropertyChanged(nameof(IsTop));
            }
        }
    }

    private Message repost;
    public Message Repost
    {
        get { return repost; }
        set
        {
            if (repost != value)
            {
                repost = value;
                OnPropertyChanged(nameof(Repost));
            }
        }
    }

    private string text;
    public string Text
    {
        get { return text; }
        set
        {
            if (text != value)
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    private bool inited = false;
    public bool Inited
    {
        get { return inited; }
        set
        {
            if (inited != value)
            {
                inited = value;
                OnPropertyChanged(nameof(Inited));
            }
        }
    }

    private List<Media> medias = new List<Media>();
    public List<Media> Medias
    {
        get { return medias; }
        set
        {
            if (medias != value)
            {
                medias = value;
                OnPropertyChanged(nameof(Medias));
            }
        }
    }

    private object tag;
    public object Tag
    {
        get { return tag; }
        set
        {
            if (tag != value)
            {
                tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }
    }

    public int CompareTo(Message other)
    {
        if (other == null)
        {
            return 1;
        }
        return other.CreateAt.CompareTo(this.CreateAt); //降序
    }

    public virtual void Init()
    {
        if (Inited)
        {
            return;
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
