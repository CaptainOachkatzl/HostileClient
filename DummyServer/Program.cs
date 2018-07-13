using HostileClient;
using System.Net;
using System.Net.Sockets;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Accepters;
using XSLibrary.Network.Connections;
using XSLibrary.ThreadSafety.Containers;
using XSLibrary.Utility;

namespace DummyServer
{
    class Program
    {
        static SafeList<IConnection> _connections = new SafeList<IConnection>();
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
            TCPPacketConnection connection = new TCPPacketConnection(acceptedSocket);
            connection.Logger = logger;
            _connections.Add(connection);
            connection.InitializeCrypto(new RSACrypto(false));

            //DualConnection connection = new DualConnection(acceptedSocket, IPAddress.Any);
            //connection.Logger = logger;
            //_connections.Add(connection);
            //connection.Initialize(false);
        }
    }
}
