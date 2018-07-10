using System;

namespace HostileClient
{
    class RandomDataSpam : PacketSpam
    {
        Random random = new Random();

        public RandomDataSpam()
        {
        }

        protected override byte[] CreateData(int length)
        {
            byte[] data = new byte[length];
            random.NextBytes(data);
            return data;
        }
    }
}
