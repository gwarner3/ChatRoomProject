using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TextLogger logger = new TextLogger();
            new Server(logger).Run();
            Console.ReadLine();
        }
    }
}
