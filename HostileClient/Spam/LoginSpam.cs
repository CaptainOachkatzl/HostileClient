using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connections;
using XSLibrary.Network.Connectors;

namespace HostileClient.Spam
{
    class LoginSpam : ISpam
    {
        protected override void SpamAction(int index)
        {
            AccountConnector connector = new AccountConnector();
            connector.Logger = Logger;
            connector.Crypto = CryptoType.RSALegacy;

            connector.ConnectAsync(Target, (connection) => { connection.Disconnect(); }, () => { });
        }
    }
}
