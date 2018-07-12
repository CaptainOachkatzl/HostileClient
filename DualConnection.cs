﻿using System;
using System.Net;
using System.Net.Sockets;
using XSLibrary.Cryptography.ConnectionCryptos;
using XSLibrary.Network.Connections;
using XSLibrary.Utility;

namespace HostileClient
{
    public class DualConnection
    {
        public event IConnection.CommunicationErrorHandler OnDisconnect;

        TCPConnection tcpConnetion;
        UDPConnection udpConnection;

        public Logger Logger { get; set; } = new NoLog();

        public DualConnection(Socket socket, IPAddress ip = null)
        {
            tcpConnetion = new TCPConnection(socket);

            if (ip != null)
                udpConnection = new UDPConnection(new IPEndPoint(ip, (socket.LocalEndPoint as IPEndPoint).Port));
            else
                udpConnection = new UDPConnection(socket.LocalEndPoint);

            udpConnection.SetDefaultSend(socket.RemoteEndPoint);

            tcpConnetion.OnDisconnect += HandleTCPDisconnect;
            tcpConnetion.DataReceivedEvent += TcpConnetion_DataReceivedEvent;
            udpConnection.DataReceivedEvent += UdpConnection_DataReceivedEvent;
        }

        public void Disconnect()
        {
            tcpConnetion.Disconnect();
        }

        public void Initialize(bool active)
        {
            udpConnection.Logger = Logger;
            tcpConnetion.Logger = Logger;

            if(!udpConnection.InitializeCrypto(new ECCrypto(active)))
            {
                tcpConnetion.Disconnect();
                return;
            }

            tcpConnetion.InitializeReceiving();
            udpConnection.InitializeReceiving();
        }

        public void SendTCP(byte[] data)
        {
            tcpConnetion.Send(data);
        }

        public void SendUDP(byte[] data)
        {
            udpConnection.Send(data);
        }

        public void SendDual(byte[] data)
        {


            SendTCP(data);
            SendUDP(data);
        }

        private void UdpConnection_DataReceivedEvent(object sender, byte[] data, EndPoint source)
        {
            Logger.Log("Received UDP data from {0}.", source.ToString());
        }

        private void TcpConnetion_DataReceivedEvent(object sender, byte[] data, EndPoint source)
        {
            Logger.Log("Received TCP data from {0}.", source.ToString());
        }

        private void HandleTCPDisconnect(object sender, EndPoint remote)
        {
            udpConnection.Disconnect();
            OnDisconnect?.Invoke(this, remote);
        }
    }
}
