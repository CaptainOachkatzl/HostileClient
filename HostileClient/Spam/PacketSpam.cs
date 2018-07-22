using System;
using System.Net.Sockets;
using XSLibrary.Network.Connections;

namespace HostileClient.Spam
{
    abstract class PacketSpam : ConnectionSpam
    {
        protected TCPPacketConnection Connection { get; set; }
        public int Length { get; set; } = 65;

        public PacketSpam()
        {
        }

        protected override void CreateActions(Socket socket)
        {
            Connection = new TCPPacketConnection(socket);
            Connection.Logger = Logger;
        }

        protected override void InitActions()
        {
        }

        protected override void ExecutionActions()
        {
            Connection.Send(CreateData(Length));
        }

        protected override void CleanUpActions()
        {
            Connection.Disconnect();
        }

        protected abstract byte[] CreateData(int length);
    }
}
