using System;
/**
 * Document: Game.cs
 * Description: Gère la totalité du jeu
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Threading;
using TPI.Engine;
using TPI.Entities;
using TPI.Network;

namespace TPI
{
    /// <summary>
    /// Rassemble tous les objets du jeu
    /// </summary>
    public class Game
    {
        private static Level _currentLevel;
        private bool _full = false;

        private Character _player, _competitor;
        private bool _running;
        private float XScroll = 0f;
        private NetworkManager _netManager;
        private RecieveManager _recieveManager;
        private long _gameID;
        private int _playerID = 1;

        /// <summary>
        /// Initalise le jeu
        /// </summary>
        /// <param name="pJoin">True si le joueur rejoint une partie, false si il la crée</param>
        /// <param name="pIP">adresse ip de la partie</param>
        /// <param name="pName">Pseudo du joueur</param>
        public Game(bool pJoin, string pIP, string pName)
        {
            CurrentLevel = new Level(-1, !pJoin);

            Running = true;
            Player = new Character(Color.Blue, new Vector2f());
            Competitor = new Character(Color.FromArgb(128, Color.Red), new Vector2f());

            IPAddress ip;
            IPAddress.TryParse(pIP, out ip);

            GameID = pJoin ? 0 : GetTimeMicroSeconds();

            RecieveManager = new Network.RecieveManager(!pJoin, GameID, CurrentLevel, Competitor, this);
            NetManager = new NetworkManager(ip, RecieveManager);

            if (pJoin)
            {
                NetManager.Send("000");
                PlayerID = 0;
                CurrentLevel.Elements.Clear();
                Full = true;
            }
        }

        /// <summary>
        /// Lance le rendu du niveau et des deux personnages
        /// </summary>
        public void Render()
        {
            GraphicsState gs = Entity.Context.Save();
            Entity.Context.TranslateTransform(-XScroll + 100, 0);
            CurrentLevel.Render();
            Player.Render();
            Competitor.Render();
            Entity.Context.Restore(gs);
        }

        /// <summary>
        /// Lance la mise à jour du niveau et du personnage du joueur
        /// </summary>
        public void Update()
        {
            long lastUp = 0;
            Stopwatch stopWatchUp = Stopwatch.StartNew();
            int netCooldown = 0;
            while (Running)
            {
                netCooldown++;
                CurrentLevel.Update();
                Player.Update();
                this.XScroll = Player.Position.X;

                if (netCooldown % (Constants.UPDATE_CAP / 40) == 0)
                {
                    netCooldown = 0;
                    if (Full)
                        NetManager.Send("010 " + GameID + " " + PlayerID + " " + Player.Position.X + "-" + Player.Position.Y);
                }

                CapLoop(Constants.UPDATE_CAP * Constants.GAME_SPEED, stopWatchUp.ElapsedMilliseconds - lastUp);
                lastUp = stopWatchUp.ElapsedMilliseconds;
            }

            NetManager.StopListening();
        }

        /// <summary>
        /// bloque le thread en fonction du temps d’exécution d’une boucle et du nombre d’exécution désirée par seconde
        /// </summary>
        /// <param name="pCap">Nombre de tours par seconde</param>
        /// <param name="pExecutionTime">Dernier temps d'execution</param>
        public static void CapLoop(float pCap, long pExecutionTime)
        {
            float Millis = (1000f / (float)pCap);
            int sleepTime = (int)(Millis - pExecutionTime);
            if (sleepTime > 0)
                Thread.Sleep(sleepTime);
        }

        /// <summary>
        /// Donne l'heure en microsecondes
        /// </summary>
        /// <returns></returns>
        public static long GetTimeMicroSeconds()
        {
            return Convert.ToInt64(DateTime.Now.ToString("HHmmssffffff"));
        }

        /// <summary>
        /// Niveau actuel
        /// </summary>
        public static Level CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }

        /// <summary>true si la partie est pleine (2 joueurs), false sinon</summary>
        public bool Full
        {
            get { return _full; }
            set { _full = value; }
        }
        /// <summary>Personnage représentant le joueur local</summary>
        public Character Player
        {
            get { return _player; }
            set { _player = value; }
        }
        /// <summary>ID du joueur local, 1 s'il est l'hôte, 0 sinon</summary>
        public int PlayerID
        {
            get { return _playerID; }
            set { _playerID = value; }
        }
        /// <summary>Personnage représentant le joueur distant</summary>
        public Character Competitor
        {
            get { return _competitor; }
            set { _competitor = value; }
        }
        /// <summary>ID de la partie</summary>
        public long GameID
        {
            get { return _gameID; }
            set { _gameID = value; }
        }
        /// <summary>true si le programme est en cours d'execution</summary>
        public bool Running
        {
            get { return _running; }
            set { _running = value; }
        }
        /// <summary>Gestionnaire du réseau</summary>
        public NetworkManager NetManager
        {
            get { return _netManager; }
            set { _netManager = value; }
        }
        /// <summary>Gestionnaire des messages entrants</summary>
        public RecieveManager RecieveManager
        {
            get { return _recieveManager; }
            set { _recieveManager = value; }
        }
    }
}
