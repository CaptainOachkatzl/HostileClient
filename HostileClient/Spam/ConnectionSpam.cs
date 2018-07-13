using System.Net;
using System.Net.Sockets;
using XSLibrary.Utility;

namespace HostileClient
{
    class ConnectionSpam
    {
        public EndPoint Target { get; set; }
        public int Count { get; set; } = 1;
        public Logger Logger { get; set; } = new NoLog();

        public ConnectionSpam()
        {
        }

        public void Run()
        {
            for (int i = 0; i < Count; i++)
            {
                Logger.Log("Connecting #{0}", i);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(Target);

                CreateActions(socket);

                InitActions();

                ExecutionActions();

                CleanUpActions();
            }
        }

        protected virtual void CreateActions(Socket socket)
        {

        }

        protected virtual void InitActions()
        {

        }

        protected virtual void ExecutionActions()
        {

        }

        protected virtual void CleanUpActions()
        {

        }
    }
}
