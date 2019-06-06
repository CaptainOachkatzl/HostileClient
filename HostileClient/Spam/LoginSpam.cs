using System.Threading;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connectors;
using XSLibrary.Utility;

namespace HostileClient.Spam
{
    class LoginSpam : NetworkSpam
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
                connectors[i].Crypto = /*i % 3 != 0 ?*/ CryptoType.RSALegacy; // : CryptoType.EC;

                switch (i % 3)
                {
                    case 0:
                        connectors[i].Login = "adrian hackerman";
                        break;
                    case 1:
                        connectors[i].Login = "business casual";
                        break;
                    default:
                        connectors[i].Login = "nerd kek";
                        break;
                }
            }

            for (int i = 0; i < finishEvents.Length; i++)
            {
                finishEvents[i] = new ManualResetEvent(false);
            }
        }

        protected override void SpamAction(int index)
        {
            connectors[index].ConnectAsync
                (
                Target, 
                (connection) =>
                {
                    Logger.Log(LogLevel.Priority, "Connected successfully.");
                    finishEvents[index].Set();
                    Thread.Sleep(1000);
                    connection.Disconnect();
                },
                () => { finishEvents[index].Set();
                });
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
