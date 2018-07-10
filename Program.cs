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
                BigDataSpam bigDataSpam = new BigDataSpam();
                bigDataSpam.Logger = new LoggerConsole();
                bigDataSpam.Target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 22222);
                bigDataSpam.Count = 1;
                bigDataSpam.Run();

                continue;

                ConnectionSpam connectionSpam = new RandomDataSpam();
                connectionSpam.Logger = new LoggerConsole();
                connectionSpam.Count = 100;
                connectionSpam.Target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 22222);

                connectionSpam.Run();
            }
        }
    }
}
