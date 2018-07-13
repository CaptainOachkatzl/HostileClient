using HostileClient;
using System.Net;
using System.Net.Sockets;
using XSLibrary.Network.Accepters;
using XSLibrary.ThreadSafety.Containers;
using XSLibrary.Utility;

namespace DummyServer
{
    class Program
    {
        static SafeList<DualConnection> _connections = new SafeList<DualConnection>();
        static Logger logger = new LoggerConsole();

        static void Main(string[] args)
        {
            TCPAccepter accepter = new TCPAccepter(1234, 10);
            accepter.Logger = logger;
            accepter.ClientConnected += Accepter_ClientConnected;
            accepter.Run();

            while (true) ;
        }

        private static void Accepter_ClientConnected(object sender, Socket acceptedSocket)
        {
            DualConnection connection = new DualConnection(acceptedSocket, IPAddress.Any);
            connection.Logger = logger;
            _connections.Add(connection);
            connection.Initialize(false);
        }
    }
}
