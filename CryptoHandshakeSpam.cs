using System.Net;
using System.Net.Sockets;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connections;
using XSLibrary.Utility;

namespace HostileClient
{
    class CryptoHandshakeSpam : ConnectionSpam
    {
        public CryptoHandshakeSpam()
        {
        }

        protected override void PostConnectActions(Socket socket)
        {
            IConnectionCrypto Crypto = new ECCrypto(true);
            TCPPacketConnection connection = new TCPPacketConnection(socket);
            connection.Logger = Logger;
            connection.InitializeCrypto(Crypto);
        }
    }
}
