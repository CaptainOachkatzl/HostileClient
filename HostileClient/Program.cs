using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connections;
using XSLibrary.Utility;

namespace HostileClient
{
    class Program
    {
        static Logger logger = new LoggerConsole();
        static DualConnection dualConnection = null;
        static TCPPacketConnection packetConnection = null;
        static List<Thread> threads = new List<Thread>();
        static bool m_abort = false;

        static void Main(string[] args)
        {
            IPEndPoint target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 22223);
            int count = 1;

            List<ConnectionSpam> spams = new List<ConnectionSpam>();
            //spams.Add(new ConnectionSpam());
            //spams.Add(new BigDataSpam());
            //spams.Add(new RandomDataSpam());

            while (Console.In.ReadLine() != "exit")
            {
                Disconnect();

                //Thread.Sleep(1000);

                m_abort = false;

                logger.Log("Connecting...");

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(target);

                packetConnection = new TCPPacketConnection(socket);
                packetConnection.Logger = logger;
                packetConnection.InitializeCrypto(new RSALegacyCrypto(true));

                //Thread keepalive = new Thread(KeepAliveLoop);
                //threads.Add(keepalive);
                //keepalive.Start();


                //for (int i = 0; i < threads.Length; i++)
                //{
                //    threads[i] = new Thread(SendLoop);
                //    threads[i].Start();
                //}

                //Thread.Sleep(5000);

                //Disconnect();

                //dualConnection = new DualConnection(socket);
                //dualConnection.Logger = logger;
                //dualConnection.OnDisconnect += HandleDisconnect;
                //dualConnection.Initialize(true);
                //dualConnection.SendDual(new byte[] { 0, 1, 2, 3 });


                foreach (ConnectionSpam spam in spams)
                {
                    spam.Logger = logger;
                    spam.Target = target;
                    spam.Count = count;
                    spam.Run();
                }
            }

            Disconnect();
        }

        static private void SendLoop()
        {
            while(!m_abort)
            {
                packetConnection.Send(RandomDataSpam.GenerateRandomData(100)/*Encoding.ASCII.GetBytes("Hallo Adrian")*/);
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

        static private void HandleDisconnect(object sender, EndPoint remote)
        {
            logger.Log("Dualconnection disconnected.\n\n");
        }

        static private void Disconnect()
        {
            m_abort = true;
            foreach (Thread thread in threads)
                thread?.Join();

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
