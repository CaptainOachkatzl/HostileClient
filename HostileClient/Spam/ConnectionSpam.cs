using System.Net.Sockets;
using XSLibrary.Utility;

namespace HostileClient.Spam
{
    class ConnectionSpam : ISpam
    {
        public ConnectionSpam()
        {
        }

        protected override void SpamAction(int index)
        {
            Logger.Log(LogLevel.Information, "Connecting #{0}", index);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(Target);
            }
            catch { return; }

            CreateActions(socket);

            InitActions();

            ExecutionActions();

            CleanUpActions();
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
