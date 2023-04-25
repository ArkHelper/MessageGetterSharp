namespace Test
{
    using MessageGetter;

    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {
                for(; ; )
                {
                    try
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine("test");
                    }
                    catch
                    {
                        
                    }
                }
            });
            thread.Start();
            Thread.Sleep(6000);
            Console.WriteLine("start to kill");
            thread.Interrupt();

            Console.WriteLine("exit");

        }
    }
}