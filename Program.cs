using System;
using System.Net;
using XSLibrary.Utility;

namespace HostileClient
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Console.In.ReadLine() != "exit")
            {
                ConnectionSpam connectionSpam = new ConnectionSpam();
                connectionSpam.Logger = new LoggerConsole();
                connectionSpam.Count = 100;
                connectionSpam.Target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 22222);

                connectionSpam.Run();
            }
        }
    }
}
