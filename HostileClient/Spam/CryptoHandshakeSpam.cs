using System.Net;
using System.Net.Sockets;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connections;
using XSLibrary.Utility;

namespace HostileClient.Spam
{
    class CryptoHandshakeSpam : ConnectionSpam
    {
        IConnection Connection;

        public CryptoHandshakeSpam()
        {
        }

        protected override void CreateActions(Socket socket)
        {
            Connection = new TCPPacketConnection(socket);
            Connection.Logger = Logger;
        }

        protected override void InitActions()
        {
            IConnectionCrypto Crypto = new ECCrypto(true);
            Connection.InitializeCrypto(Crypto);
        }

        protected override void CleanUpActions()
        {
            Connection.Disconnect();
        }
    }
}
