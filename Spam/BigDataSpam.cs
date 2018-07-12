using System.Threading;
using XSLibrary.Cryptography.ConnectionCryptos;

namespace HostileClient
{
    class BigDataSpam : PacketSpam
    {
        byte symbol = 0x55;

        byte[] Data1;
        byte[] Data2;
        public BigDataSpam()
        {
            Length = 1024;
        }

        protected override void InitActions()
        {
            symbol = (byte)'A';
            Data1 = CreateData(Length);

            symbol = (byte)'B';
            Data2 = CreateData(Length);
            
            Connection.InitializeCrypto(new ECCrypto(true));
        }

        protected override void ExecutionActions()
        {
            Thread thread1 = new Thread(() => Connection.Send(Data1));
            Thread thread2 = new Thread(() => Connection.Send(Data2));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        protected override byte[] CreateData(int length)
        {
            byte[] data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = symbol;
            }

            return data;
        }
    }
}
