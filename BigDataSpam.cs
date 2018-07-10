using XSLibrary.Cryptography.ConnectionCryptos;

namespace HostileClient
{
    class BigDataSpam : PacketSpam
    {
        public BigDataSpam()
        {
            Length = 4096;
        }

        protected override void InitActions()
        {
            Connection.MaxPackageSendSize = 1024;
            //Connection.InitializeCrypto(new ECCrypto(true));
        }

        protected override byte[] CreateData(int length)
        {
            byte[] data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = (byte)(i % 256);
            }

            return data;
        }
    }
}
