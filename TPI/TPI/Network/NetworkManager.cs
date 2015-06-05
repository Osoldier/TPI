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

namespace TPI.Network {

    /// <summary>
    /// Gère la connexion à l'adversaire
    /// </summary>
    public class NetworkManager
    {
        /// <summary>Port de connexion</summary>
        private const int PORT = 2015;

        private UdpClient _client;
        private IPAddress _multicastIpAddress;
        private IPEndPoint _localEndPoint;
        private IPEndPoint _remoteEndPoint;
        private byte[] _buffer;
        private bool _listening;
        private Thread _listener;
        private NetworkRecieveCallback _callback;

        /// <summary>
        /// Crée le manager et commence a écouter
        /// </summary>
        /// <param name="pIP">Adresse IP à écouter</param>
        /// <param name="pCallback">Objet contenant l'implémentation de OnRecieve à appler chaque fois qu'une message est reçu</param>
        public NetworkManager (IPAddress pIP, NetworkRecieveCallback pCallback)
	    {
            this.MulticastIpAddress = pIP;
            this.Callback = pCallback;

            this.RemoteEndPoint = new IPEndPoint(this.MulticastIpAddress, PORT);
            this.LocalEndPoint = new IPEndPoint(IPAddress.Any, PORT);

            this.Client = new UdpClient();
            this.Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.Client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 127);
            this.Client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, 127);
            this.Client.ExclusiveAddressUse = false;
            this.Client.Client.Bind(this.LocalEndPoint);
            this.Client.JoinMulticastGroup(this.MulticastIpAddress);

            this.StartListening();
	    }

        /// <summary>
        /// Arrête l'écoute
        /// </summary>
        ~NetworkManager()
        {
            this.StopListening();
        }

        /// <summary>
        /// Converti un table de bytes en string (ASCII)
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] pData)
        {
            return Encoding.ASCII.GetString(pData);
        }

        /// <summary>
        /// Converti une chaine de caractères en table de byte (ASCII)
        /// </summary>
        /// <param name="pData"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string pData)
        {
            return Encoding.ASCII.GetBytes(pData);
        }

        /// <summary>
        /// Débute l'écoute sur le réseau
        /// </summary>
        public void StartListening()
        {
            this.Listening = true;
            this.Listener = new Thread(new ThreadStart(this.Receive));
            this.Listener.IsBackground = true;
            this.Listener.Start();
        }

        /// <summary>
        /// Arrête l'écoute sur le réseau
        /// </summary>
        public void StopListening()
        {
            this.Listening = false;
            this.Listener.Join();
            this.Client.Client.Close();
            this.Client.Close();
        }

        /// <summary>
        /// Envoie un message
        /// </summary>
        /// <param name="pData">Message à envoyer</param>
        public void Send(string pData)
        {
            this.Client.Send(StringToBytes(pData), pData.Length, this.RemoteEndPoint);
        }

        /// <summary>
        /// Attend le reçu d'un message
        /// </summary>
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

        /// <summary>
        /// Client UDP
        /// </summary>
        public UdpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }

        /// <summary>
        /// Adresse IP de multicast
        /// </summary>
        public IPAddress MulticastIpAddress
        {
            get { return _multicastIpAddress; }
            set { _multicastIpAddress = value; }
        }

        /// <summary>
        /// Point d'attache local
        /// </summary>
        public IPEndPoint LocalEndPoint
        {
            get { return _localEndPoint; }
            set { _localEndPoint = value; }
        }

        /// <summary>
        /// Point d'attache distant
        /// </summary>
        public IPEndPoint RemoteEndPoint
        {
            get { return _remoteEndPoint; }
            set { _remoteEndPoint = value; }
        }

        /// <summary>
        /// Reçu du réseau
        /// </summary>
        public byte[] Buffer
        {
            get { return _buffer; }
            set { _buffer = value; }
        }

        /// <summary>
        /// Le réseau doit écouter ?
        /// </summary>
        public bool Listening
        {
            get { return _listening; }
            set { _listening = value; }
        }

        /// <summary>
        /// Thread de réception
        /// </summary>
        public Thread Listener
        {
            get { return _listener; }
            set { _listener = value; }
        }

        /// <summary>
        /// Objet contenant l'implémentation de OnRecieve à appler chaque fois qu'une message est reçu
        /// </summary>
        public NetworkRecieveCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }
    }
}
