/**
 * Document: TCPCommunicator.csserver
 * Description: Gère les envois tcp
 * Auteur: Ibanez Thomas
 * Date: 08.09.15
 * Version: 0.1
 */
using System;
using System.Net;
using System.Net.Sockets;

namespace TPI.Network
{
    /// <summary>
    /// Gère les envois tcp
    /// </summary>
    public class TCPCommunicator
    {
        TcpClient _client = null;
        TcpListener _listener = null;
        Socket _server = null;
        NetworkStream _stream;
        private const int PORT = 2015;

        /// <summary>
        /// Ecoute ou se connecte à une adresse IP.
        /// </summary>
        /// <param name="pJoin">Si <c>true</c> attend, sinon se connecte.</param>
        /// <param name="pIP">L'ip d'échange</param>
        public TCPCommunicator(bool pJoin, string pIP)
        {
            if (pJoin)
            {
                Call(pIP);
            }
            else
            {
                Listen(pIP);
            }
        }

        /// <summary>
        /// Tente de se connecter sur à une adresse ip
        /// </summary>
        /// <param name="pIP"></param>
        public void Call(string pIP)
        {
            Client = new TcpClient(pIP, PORT);
            Stream = Client.GetStream();
        }

        /// <summary>
        /// écoute et attend une connection sur une ip
        /// </summary>
        /// <param name="pIP"></param>
        public void Listen(string pIP)
        {
            Listener = new TcpListener(IPAddress.Parse(pIP), PORT);
            Listener.Start();
            Server = Listener.AcceptSocket();
            Stream = new NetworkStream(Server);
        }

        /// <summary>
        /// Envoie des donnés à travers le réseau
        /// </summary>
        /// <param name="pData"></param>
        public void Send(byte[] pData)
        {
            Stream.Write(pData, 0, pData.Length);
        }

        /// <summary>
        /// Vérifie si des donnés doivent etre traités
        /// </summary>
        /// <returns>System.String</returns>
        public string CheckIncomingMessages()
        {

            byte[] b = new byte[100];

            int bytesRead = 0;
            Stream.ReadTimeout = 1;

            try
            {
                if (Stream.DataAvailable)
                    bytesRead = Stream.Read(b, 0, 100);
            }

            catch (Exception)
            {
                return "";
            }

            if (bytesRead <= 0)
                return "";

            return NetworkManager.BytesToString(b);
        }

        /// <summary>
        /// Ferme les connections et les flux
        /// </summary>
        public void CleanUp()
        {
            if (Server != null)
            {
                Server.Close();
                Server = null;
            }
            if (Client != null)
            {
                Client.Close();
                Client = null;
            }
            if (Listener != null)
            {
                Listener.Stop();
                Listener = null;
            }
            if (Stream != null)
            {
                Stream.Close();
            }
        }

        /// <summary>Client TCP</summary>
        public TcpClient Client
        {
            get { return _client; }
            set { _client = value; }
        }
        /// <summary>Gestionnaire de connexion TCP</summary>
        public TcpListener Listener
        {
            get { return _listener; }
            set { _listener = value; }
        }
        /// <summary>Serveur TCP</summary>
        public Socket Server
        {
            get { return _server; }
            set { _server = value; }
        }
        /// <summary>Flux réseau</summary>
        public NetworkStream Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }
    }
}
