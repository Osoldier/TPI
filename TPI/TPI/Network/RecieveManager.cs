using System;
/**
 * Document: RecieveManager.cs
 * Description: Enfant de NetworkRecieveCallback, gère les messages reçus
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System.Diagnostics;
using TPI.Engine;
using TPI.Entities;

namespace TPI.Network
{
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

        private bool isHost;
        private long ID;
        private Level level;
        private Character competitor;
        private Game game;
        private bool locked = false;

        public RecieveManager(bool pIsHost, long pID, Level pLevel, Character pComp, Game pGame)
        {
            this.level = pLevel;
            this.isHost = pIsHost;
            this.ID = pID;
            this.competitor = pComp;
            this.game = pGame;
        }

        public void OnRecieve(byte[] data)
        {
            string strData = NetworkManager.BytesToString(data);
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
            }
        }

        private void HandleJoinRequest()
        {
            if (isHost && !locked)
            {
                locked = true;
                game.NetManager.Send(ID_RETURN + " " + ID);
                SendLevelInfos();
            }
        }

        private void HandleIDReturn(string pId)
        {
            if (isHost) return;
            long id = Int64.Parse(pId);
            this.ID = id;
        }

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

            Game.Full = true;
        }

        private void HandleCompetitorUpdate(string pInfos)
        {
            string[] infos = pInfos.Split(' ');

            if (Int64.Parse(infos[0]) != ID) return;

            int w = Int32.Parse(infos[1]);
            if ((w == 1 && isHost) || (w == 0 && !isHost)) return;

            float x = float.Parse(infos[2].Split('-')[0]);
            float y = float.Parse(infos[2].Split('-')[1]);

            competitor.Position.X = x;
            competitor.Position.Y = y;
        }

        private void SendLevelInfos()
        {
            foreach (Platform ptf in level.Elements)
            {
                string info = PLATFORM_UPDATE + " " + ID + " " + (ptf.End ? "1" : "0") + " " + ptf.Position.X + "-" + ptf.Position.Y + " " + ptf.Size.X + "-" + ptf.Size.Y;
                game.NetManager.Send(info);
            }
        }
    }
}
