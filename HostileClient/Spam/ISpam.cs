using System.Net;
using XSLibrary.Utility;

namespace HostileClient.Spam
{
    public abstract class ISpam
    {
        public EndPoint Target { get; set; }
        public int Count { get; set; } = 1;
        public Logger Logger { get; set; } = Logger.NoLog;

        public void Run()
        {
            for (int i = 0; i < Count; i++)
            {
                SpamAction(i);
            }
        }

        protected abstract void SpamAction(int index);
    }
}
