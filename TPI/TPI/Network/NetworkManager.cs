/**
 * Document: NetworkManager.cs
 * Description: Gère la connexion à l'adversaire
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System.Net.Sockets;
using System.Threading;
using System;
using System.Net;
using System.Text;

namespace TPI.Network
{
    public class NetworkManager
    {
        private const int PORT = 2015;

        private UdpClient _client;
        private IPAddress _multicastIpAddress;
        private IPEndPoint _localEndPoint;
        private IPEndPoint _remoteEndPoint;
        private byte[] _buffer;
        private bool _listening;
        private Thread _listener;
        private NetworkRecieveCallback _callback;

        public NetworkManager (IPAddress pIP, NetworkRecieveCallback pCallback)
	    {
            this.MulticastIpAddress = pIP;
            this.Callback = pCallback;

            this.RemoteEndPoint = new IPEndPoint(this.MulticastIpAddress, PORT);
            this.LocalEndPoint = new IPEndPoint(IPAddress.Any, PORT);

            this.Client = new UdpClient();
            this.Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.Client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 10);
            this.Client.ExclusiveAddressUse = false;
            this.Client.Client.Bind(this.LocalEndPoint);
            this.Client.JoinMulticastGroup(this.MulticastIpAddress);

            this.StartListening();
	    }

        ~NetworkManager()
        {
            this.StopListening();
        }

        public static string BytesToString(byte[] pData)
        {
            return Encoding.ASCII.GetString(pData);
        }

        public static byte[] StringToBytes(string pData)
        {
            return Encoding.ASCII.GetBytes(pData);
        }

        public void StartListening()
        {
            this.Listening = true;
            this.Listener = new Thread(new ThreadStart(this.Receive));
            this.Listener.IsBackground = true;
            this.Listener.Start();
        }

        public void StopListening()
        {
            this.Listening = false;
            this.Listener.Join();
        }

        public void Send(string pData)
        {
            this.Client.Send(StringToBytes(pData), pData.Length, this.RemoteEndPoint);
        }

        public void Receive()
        {
            while (this.Listening)
            {
                if (this.Client.Available > 0)
                {
                    this.Buffer = this.Client.Receive(ref _localEndPoint);
                    this.Callback.OnRecieve(this.Buffer);
                }
                Thread.Sleep(1);
            }
        }

        public UdpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }
        public IPAddress MulticastIpAddress
        {
            get { return _multicastIpAddress; }
            set { _multicastIpAddress = value; }
        }
        public IPEndPoint LocalEndPoint
        {
            get { return _localEndPoint; }
            set { _localEndPoint = value; }
        }
        public IPEndPoint RemoteEndPoint
        {
            get { return _remoteEndPoint; }
            set { _remoteEndPoint = value; }
        }
        public byte[] Buffer
        {
            get { return _buffer; }
            set { _buffer = value; }
        }
        public bool Listening
        {
            get { return _listening; }
            set { _listening = value; }
        }
        public Thread Listener
        {
            get { return _listener; }
            set { _listener = value; }
        }
        public NetworkRecieveCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }
    }
}
