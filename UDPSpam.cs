using System;
using System.Net;
using System.Net.Sockets;
using XSLibrary.Network.Connections;

namespace HostileClient
{
    abstract class UDPSpam : ConnectionSpam
    {
        protected UDPConnection Connection { get; set; }
        public int Length { get; set; } = 65;

        public UDPSpam()
        {
        }

        protected override void CreateActions(Socket socket)
        {
            Connection = new UDPConnection(socket.LocalEndPoint as IPEndPoint);
            Connection.SetDefaultSend(socket.RemoteEndPoint as IPEndPoint);
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
