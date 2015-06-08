/**
 * Document: UDPBroadcaster.cs
 * Description: Gère les envois udp
 * Auteur: Ibanez Thomas
 * Date: 08.09.15
 * Version: 0.1
 */
using System.Net;
using System.Net.Sockets;

namespace TPI.Network
{
    /// <summary>
    /// Gère les envois udp
    /// </summary>
    public class UDPBroadcaster
    {
        /// <summary>Port de connexion</summary>
        private const int PORT = 2015;

        private UdpClient _client;
        private IPAddress _multicastIpAddress;
        private IPEndPoint _localEndPoint;
        private IPEndPoint _remoteEndPoint;
        private byte[] _buffer;

        /// <summary>
        /// Crée un broadcaster UDP à l'adresse 255.0.0.1
        /// </summary>
        public UDPBroadcaster()
        {
            this.MulticastIpAddress = IPAddress.Parse("225.0.0.1");

            this.RemoteEndPoint = new IPEndPoint(this.MulticastIpAddress, PORT);
            this.LocalEndPoint = new IPEndPoint(IPAddress.Any, PORT);

            this.Client = new UdpClient();
            this.Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.Client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 127);
            this.Client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, 127);
            this.Client.ExclusiveAddressUse = false;
            this.Client.Client.Bind(this.LocalEndPoint);
            this.Client.JoinMulticastGroup(this.MulticastIpAddress);
        }

        /// <summary>
        /// Envoie des données en multicast
        /// </summary>
        /// <param name="pData">Données à envoyer</param>
        public void Send(byte[] pData)
        {
            this.Client.Send(pData, pData.Length, this.RemoteEndPoint);
        }

        public string CheckIncomingMessages()
        {
            if (this.Client.Available > 0)
            {
                this.Buffer = this.Client.Receive(ref _localEndPoint);
                return NetworkManager.BytesToString(Buffer);
            }
            return "";
        }

        /// <summary>
        /// Ferme la connexion
        /// </summary>
        public void CleanUp()
        {
            this.Client.Client.Close();
            this.Client.Close();
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
    }
}
