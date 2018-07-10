using System.Net;
using System.Net.Sockets;
using XSLibrary.Utility;
using XSLibrary.Network.Connections;
using XSLibrary.Cryptography.ConnectionCryptos;

namespace HostileClient
{
    class ConnectionSpam
    {
        public EndPoint Target { get; set; }
        public int Count { get; set; } = 1;
        public Logger Logger { get; set; } = new NoLog();
        protected ConnectionInterface Connection { get; set; }

        public ConnectionSpam()
        {
        }

        public void Run()
        {
            for (int i = 0; i < Count; i++)
            {
                Logger.Log("Connecting #{0}", i);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(Target);

                PostConnectActions(socket);

                if(Connection != null && Connection.Connected)
                    Connection.Disconnect();
            }
        }

        protected virtual void PostConnectActions(Socket socket)
        {

        }
    }
}
