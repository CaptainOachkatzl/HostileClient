using System.Net;
using System.Net.Sockets;

namespace HostileClient
{
    class ConnectionSpam
    {
        public EndPoint Target { get; set; }
        public int Count { get; set; } = 1;

        public ConnectionSpam()
        {
        }

        public void Run()
        {
            for (int i = 0; i < Count; i++)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(Target);
                socket.Disconnect(false);
                socket.Dispose();
            }
        }
    }
}
