using System.Net;
using System.Net.Sockets;
using XSLibrary.Utility;

namespace HostileClient
{
    class ConnectionSpam
    {
        public EndPoint Target { get; set; }
        public int Count { get; set; } = 1;
        public Logger Logger { get; set; } = Logger.NoLog;

        public ConnectionSpam()
        {
        }

        public void Run()
        {
            for (int i = 0; i < Count; i++)
            {
                Logger.Log(LogLevel.Information, "Connecting #{0}", i);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(Target);
                }
                catch { continue; }

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
