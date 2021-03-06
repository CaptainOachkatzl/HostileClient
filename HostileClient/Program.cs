﻿using HostileClient.Spam;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connections;
using XSLibrary.ThreadSafety;
using XSLibrary.ThreadSafety.Events;
using XSLibrary.ThreadSafety.Executors;
using XSLibrary.ThreadSafety.Locks;
using XSLibrary.Utility;

namespace HostileClient
{
    class Program
    {
        class TestEvent : OneShotEvent<object, object> { }

        static Logger logger = new LoggerConsole();
        static IPEndPoint target = new IPEndPoint(IPAddress.Parse("192.168.0.100"), 22222);
        static List<ISpam> spams = new List<ISpam>();
        static int count = 10;
        static DualConnection dualConnection = null;
        static TCPPacketConnection packetConnection = null;
        static List<Thread> threads = new List<Thread>();
        static int threadCount = 4;
        static bool m_abort = false;

        static void Main(string[] args)
        {
            CreateSpam();
            InitSpam();

            while (Console.In.ReadLine() != "exit")
            {
                StartSpam();
            }
        }

        static void CreateSpam()
        {
            //spams.Add(new ConnectionSpam());
            //spams.Add(new BigDataSpam());
            //spams.Add(new RandomDataSpam());
            spams.Add(new LoginSpam());
            //spams.Add(new AccountCreationSpam());

            foreach (ISpam spam in spams)
            {
                spam.Logger = logger;
                spam.Count = count;

                NetworkSpam netSpam = spam as NetworkSpam;
                if(netSpam != null)
                    netSpam.Target = target;
            }
        }

        static void InitSpam()
        {
            foreach (ISpam spam in spams)
            {
                spam.Initialize();
            }
        }

        static void StartSpam()
        {
            foreach (ISpam spam in spams)
            {
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
