using System;
using System.Net;

namespace HostileClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.In.ReadLine();

            ConnectionSpam connectionSpam = new ConnectionSpam();
            connectionSpam.Count = 100;
            connectionSpam.Target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 22222);

            connectionSpam.Run();

            Console.In.ReadLine();
        }
    }
}
