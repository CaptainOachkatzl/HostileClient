using System.Net;
using XSLibrary.Utility;

namespace HostileClient.Spam
{
    public abstract class ISpam
    {
        public int Count { get; set; } = 1;
        public Logger Logger { get; set; } = Logger.NoLog;

        public virtual void Initialize()
        {
        }

        public void Run()
        {
            for (int i = 0; i < Count; i++)
            {
                SpamAction(i);
            }

            FinalizeActions();
        }

        protected abstract void SpamAction(int index);

        protected virtual void FinalizeActions()
        {
        }
    }


    public abstract class NetworkSpam : ISpam
    {
        public EndPoint Target { get; set; }
    }
}
