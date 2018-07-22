using System;

namespace HostileClient.Spam
{
    class RandomDataSpam : PacketSpam
    {
        static Random random = new Random();

        public RandomDataSpam()
        {
        }

        protected override byte[] CreateData(int length)
        {
            return GenerateRandomData(length);
        }

        static public byte[] GenerateRandomData(int length)
        {
            byte[] data = new byte[length];
            random.NextBytes(data);
            return data;
        }
    }
}
