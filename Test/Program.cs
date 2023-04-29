namespace Test
{
    using MessageGetter;
    using MessageGetter.Medias;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;

    internal class Program
    {
        static void Main(string[] args)
        {
            double per = 0;
            Getter.Configuration.RootDir = "C:\\Users\\EvATive7\\ETi7FileRepo\\develop\\MessageGetterSharp\\Test\\bin\\Debug\\net6.0";

            Picture picture = new Picture("5556678");
            picture.Link = "https://ts1.cn.mm.bing.net/th/id/R-C.d77b6556acf0862c70bb33920aab683c?rik=UKMq98NXpmWGkA&riu=http%3a%2f%2fimg95.699pic.com%2felement%2f40070%2f2720.png_860.png&ehk=Hls%2blcIA77%2b8VY570h%2fsQvyJcclZSynH2zgkg74bxmY%3d&risl=&pid=ImgRaw&r=0";

            Task t = picture.Download();

            picture.DownloadProgressChanged += (s, e) =>
            {
                per = e.ProgressPercentage;
            };

            bool a = true;
            while (a)
            {
                Thread.Sleep(20);
                Console.WriteLine("waiting:" + per);
            }
        }
    }
}