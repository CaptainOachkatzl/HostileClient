using System.Threading;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connectors;
using XSLibrary.Utility;

namespace HostileClient.Spam
{
    class LoginSpam : ISpam
    {
        AccountConnector[] connectors;
        ManualResetEvent[] finishEvents;

        public override void Initialize()
        {
            connectors = new AccountConnector[Count];
            finishEvents = new ManualResetEvent[Count];

            for (int i = 0; i < connectors.Length; i++)
            {
                connectors[i] = new AccountConnector();
                connectors[i].Logger = Logger;
                connectors[i].Crypto = i % 3 != 0 ? CryptoType.RSALegacy : CryptoType.EC;
            }

            for (int i = 0; i < finishEvents.Length; i++)
            {
                finishEvents[i] = new ManualResetEvent(false);
            }
        }

        protected override void SpamAction(int index)
        {
            connectors[index].ConnectAsync(Target, (connection) => { finishEvents[index].Set(); connection.Disconnect(); }, () => { finishEvents[index].Set(); });
        }

        protected override void FinalizeActions()
        {
            for (int i = 0; i < finishEvents.Length; i++)
            {
                finishEvents[i].WaitOne();
            }

            Logger.Log(LogLevel.Priority, "All connect attempts finished.");
        }
    }
}
