using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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


        static void Main(string[] args)
        {
            IPEndPoint target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 22222);
            int count = 1;

            List<ConnectionSpam> spams = new List<ConnectionSpam>();
            //spams.Add(new BigDataSpam());
            //spams.Add(new RandomDataSpam());

            while (Console.In.ReadLine() != "exit")
            {
                Disconnect();

                logger.Log("Connecting...");

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect("127.0.0.1", 22222);

                packetConnection = new TCPPacketConnection(socket);
                packetConnection.Logger = logger;
                packetConnection.InitializeCrypto(new ECCrypto(true));
                packetConnection.Send(new byte[] { 0, 1, 2, 3 });

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

        static private void HandleDisconnect(object sender, EndPoint remote)
        {
            logger.Log("Dualconnection disconnected.\n\n");
        }

        static private void Disconnect()
        {
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
