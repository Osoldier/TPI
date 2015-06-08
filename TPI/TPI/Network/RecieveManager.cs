/**
 * Document: RecieveManager.cs
 * Description: Enfant de NetworkRecieveCallback, gère les messages reçus
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using TPI.Engine;
using TPI.Entities;

namespace TPI.Network
{
    /// <summary>
    /// Géstionnaire de réception du réseau
    /// </summary>
    public class RecieveManager : NetworkRecieveCallback
    {
        ///<summary>Demande de connexion à une partie</summary>
        private const string JOIN_REQUEST = "000";
        ///<summary>Retour de l’id de la partie [ID = id unique de la partie] (001 ID)</summary>
        private const string ID_RETURN = "001";
        ///<summary>Position d’une plateforme [ID = id de la partie, W = 1 si la plateforme est la dernière, 0 sinon] (002 ID W X-Y L-H)</summary>
        private const string PLATFORM_UPDATE = "002";
        ///<summary>Position du joueur [ID = id de la partie, W = id du joueur (0 pour le créateur, 1 pour celui qui rejoint)] (010 ID W X-Y)</summary>
        private const string PLAYER_UPDATE = "010";
        ///<summary>Envoyé lorsqu'un joueur à gagné [ID = id de la partie, W = id du joueur (0 pour le créateur, 1 pour celui qui rejoint)] (011 ID W)</summary>
        private const string PLAYER_WON = "011";

        private bool isHost;
        private long ID;
        private Level level;
        private Character competitor;
        private Game game;
        private bool locked = false;

        /// <summary>
        /// Initialise le gestionnaire de réception réseau
        /// </summary>
        /// <param name="pIsHost">Le joueur est-il l'hôte</param>
        /// <param name="pID">ID de la partie</param>
        /// <param name="pLevel">Niveau actuel</param>
        /// <param name="pComp">Adversaire</param>
        /// <param name="pGame">Objet Game actuel</param>
        public RecieveManager(bool pIsHost, long pID, Level pLevel, Character pComp, Game pGame)
        {
            this.level = pLevel;
            this.isHost = pIsHost;
            this.ID = pID;
            this.competitor = pComp;
            this.game = pGame;
        }

        /// <summary>
        /// Gère la réception de message
        /// </summary>
        /// <param name="data"></param>
        public void OnRecieve(string data)
        {
            string strData = data;
            string code = strData.Substring(0, 3);
            string rest = "";

            if (code != JOIN_REQUEST)
                rest = strData.Substring(4);

            switch (code)
            {
                case JOIN_REQUEST:
                    HandleJoinRequest();
                    break;
                case ID_RETURN:
                    HandleIDReturn(rest);
                    break;
                case PLATFORM_UPDATE:
                    HandleLevelInfos(rest);
                    break;
                case PLAYER_UPDATE:
                    HandleCompetitorUpdate(rest);
                    break;
                case PLAYER_WON:
                    HandlePlayerWinning(rest);
                    break;
            }
        }

        /// <summary>
        /// Gère une demande de connexion
        /// </summary>
        private void HandleJoinRequest()
        {
            if (isHost && !locked)
            {
                locked = true;
                game.NetManager.SendTCP(ID_RETURN + " " + ID);
                SendLevelInfos();
                game.Full = true;
            }
        }

        /// <summary>
        /// Gère l'arrivée de l'id de la partie
        /// </summary>
        /// <param name="pId"></param>
        private void HandleIDReturn(string pId)
        {
            if (isHost) return;
            long id = Int64.Parse(pId);
            this.ID = id;
            this.game.GameID = this.ID;
        }

        /// <summary>
        /// Gère l'arrivée des positions des plateformes
        /// </summary>
        /// <param name="pId"></param>
        private void HandleLevelInfos(string pInfos)
        {
            if (isHost) return;
            string[] infos = pInfos.Split(' ');

            if (Int64.Parse(infos[0]) != ID) return;

            float x = float.Parse(infos[2].Split('-')[0]);
            float y = float.Parse(infos[2].Split('-')[1]);
            float l = float.Parse(infos[3].Split('-')[0]);
            float h = float.Parse(infos[3].Split('-')[1]);
            bool w = (Int32.Parse(infos[1]) == 1);

            Platform ptf = new Platform(new Vector2f(x, y), new Vector2f(l, h));
            ptf.End = w;
            lock (Level.CollectionLocker)
                this.level.Elements.Add(ptf);

            game.Full = true;
            Game.GameStarted = true;
            Game.Timer = Stopwatch.StartNew();
        }

        /// <summary>
        /// Gère l'arrivée de la position de l'adversaire
        /// </summary>
        /// <param name="pId"></param>
        private void HandleCompetitorUpdate(string pInfos)
        {
            string[] infos = pInfos.Split(' ');
            Debug.WriteLine(infos[0] + " " + ID + " | " + isHost);

            if (Int64.Parse(infos[0]) != ID) return;

            int w = Int32.Parse(infos[1]);
            if (w == game.PlayerID) return;

            float x = float.Parse(infos[2].Split('-')[0]);
            float y = float.Parse(infos[2].Split('-')[1]);

            competitor.Position.X = x;
            competitor.Position.Y = y;
        }

        /// <summary>
        /// Gère le message de victoire d'un joueur
        /// </summary>
        /// <param name="pInfos"></param>
        private void HandlePlayerWinning(string pInfos)
        {
            string[] infos = pInfos.Split(' ');
            if (!Game.GameStarted) return;
            if (Int64.Parse(infos[0]) != game.GameID) return;
            if (Int32.Parse(infos[1]) != game.PlayerID)
            {
                Game.Timer.Stop();
                Game.GameStarted = false;
                MessageBox.Show("Perdu !");
                game.Reset();
            }
        }

        /// <summary>
        /// Envoie les positions des plateformes
        /// </summary>
        /// <param name="pId"></param>
        private void SendLevelInfos()
        {
            foreach (Platform ptf in level.Elements)
            {
                string info = PLATFORM_UPDATE + " " + ID + " " + (ptf.End ? "1" : "0") + " " + ptf.Position.X + "-" + ptf.Position.Y + " " + ptf.Size.X + "-" + ptf.Size.Y;
                game.NetManager.SendTCP(info);
                Thread.Sleep(1000);
            }
            Game.Timer = Stopwatch.StartNew();
            Game.GameStarted = true;
        }
    }
}
