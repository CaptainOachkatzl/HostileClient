using HostileClient.Spam;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connections;
using XSLibrary.ThreadSafety;
using XSLibrary.Utility;

namespace HostileClient
{
    class Program
    {
        class TestEvent : AutoInvokeEvent<object, object> { }

        static Logger logger = new LoggerConsole();
        static IPEndPoint target = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 22222);
        static List<ISpam> spams = new List<ISpam>();
        static DualConnection dualConnection = null;
        static TCPPacketConnection packetConnection = null;
        static List<Thread> threads = new List<Thread>();
        static int threadCount = 4;
        static bool m_abort = false;
        static ManualResetEvent threadGo = new ManualResetEvent(false);

        static TestEvent OnRaise = new TestEvent();

        static void Main(string[] args)
        {

            while (Console.In.ReadLine() != "exit")
            {
                Disconnect();

                m_abort = false;

                RunThreads();
                StartSpam(100);
            }

            Disconnect();
        }

        static void StartSpam(int count)
        {
            //spams.Add(new ConnectionSpam());
            //spams.Add(new BigDataSpam());
            //spams.Add(new RandomDataSpam());
            spams.Add(new LoginSpam());

            foreach (ISpam spam in spams)
            {
                spam.Logger = logger;
                spam.Target = target;
                spam.Count = count;
                spam.Run();
            }
        }

        static void RunThreads()
        {
            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Start();
            }
        }

        static void AutoInvokeTest()
        {
            threads.Add(new Thread(() => OnRaise.Event += HandleRaise));

            for (int i = 1; i < threadCount; i++)
            {
                threads.Add(new Thread(Raise));
            }

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(target);

            packetConnection = new TCPPacketConnection(socket);
            packetConnection.Logger = logger;
            packetConnection.InitializeCrypto(new RSALegacyCrypto(true));
            packetConnection.Disconnect();
            packetConnection.OnDisconnect += HandleDisconnect;
        }

        static private void Raise()
        {
            OnRaise.Invoke(null, null);
        }

        static private void HandleRaise(object sender, object args)
        {
            Console.Out.WriteLine("Event raised.");
        }

        static private void HandleDisconnect(object sender, EndPoint remote)
        {
            Console.Out.WriteLine("Disconnect event raised.");
        }

        static private void SendLoop()
        {
            while(!m_abort)
            {
                packetConnection.Send(RandomDataSpam.GenerateRandomData(100));
            }
        }

        static private void KeepAliveLoop()
        {
            while (!m_abort)
            {
                packetConnection.SendKeepAlive();
                Thread.Sleep(1000);
            }
        }

        static private void Disconnect()
        {
            m_abort = true;
            foreach (Thread thread in threads)
                thread?.Join();

            threads.Clear();

            if (packetConnection != null)
                packetConnection.Disconnect();

            if (dualConnection != null)
            {
                dualConnection.Disconnect();
                dualConnection.OnDisconnect -= HandleDisconnect;
            }
        }
    }
}
