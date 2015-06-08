/**
 * Document: NetworkManager.cs
 * Description: Gère la connexion à l'adversaire
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace TPI.Network
{

    /// <summary>
    /// Gère la connexion à l'adversaire
    /// </summary>
    public class NetworkManager
    {
        private UDPBroadcaster _udpBroadcaster;
        private TCPCommunicator _tcpCom;
        private NetworkRecieveCallback _callback;
        private bool _listening;
        private Thread _listener;

        /// <summary>
        /// Crée le manager et commence a écouter
        /// </summary>
        /// <param name="pIP">Adresse IP à écouter</param>
        /// <param name="pCallback">Objet contenant l'implémentation de OnRecieve à appler chaque fois qu'une message est reçu</param>
        public NetworkManager(bool pJoin, IPAddress pIP, NetworkRecieveCallback pCallback)
        {
            this.Callback = pCallback;
            this.TcpCom = new TCPCommunicator(pJoin, pIP.ToString());
            this.UdpBroadcaster = new UDPBroadcaster();
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
            this.UdpBroadcaster.CleanUp();
            this.TcpCom.CleanUp();
        }

        /// <summary>
        /// Envoie un message
        /// </summary>
        /// <param name="pData">Message à envoyer</param>
        public void SendUDP(string pData)
        {
            UdpBroadcaster.Send(StringToBytes(pData));
        }

        /// <summary>
        /// Envoie un message
        /// </summary>
        /// <param name="pData">Message à envoyer</param>
        public void SendTCP(string pData)
        {
            TcpCom.Send(StringToBytes(pData));
        }

        /// <summary>
        /// Attend le reçu d'un message
        /// </summary>
        public void Receive()
        {
            while (this.Listening)
            {
                string data;
                if ((data = UdpBroadcaster.CheckIncomingMessages()) != string.Empty)
                {
                    this.Callback.OnRecieve(data);
                }
                if ((data = TcpCom.CheckIncomingMessages()) != string.Empty)
                {
                    this.Callback.OnRecieve(data);
                    Debug.WriteLine(data);
                }
            }
            Thread.Sleep(1);
        }

        public UDPBroadcaster UdpBroadcaster
        {
            get { return _udpBroadcaster; }
            set { _udpBroadcaster = value; }
        }

        public TCPCommunicator TcpCom
        {
            get { return _tcpCom; }
            set { _tcpCom = value; }
        }

        public NetworkRecieveCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        /// <summary>
        /// Le réseau doit écouter ?
        /// </summary>
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
    }
}
