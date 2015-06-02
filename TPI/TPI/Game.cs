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
    public class Game
    {
        private static Level _currentLevel;
        public static bool Full = false;

        private Character _player, _competitor;
        private bool _running;
        private float XScroll = 0f;
        private NetworkManager _netManager;
        private RecieveManager _recieveManager;
        private long _gameID;
        private int _playerID = 1;

        public Game(bool pJoin, string pIP, string pName)
        {
            CurrentLevel = new Level(-1, !pJoin);

            Running = true;
            Player = new Character(Color.Blue, new Vector2f());
            Competitor = new Character(Color.Red, new Vector2f());
            GameID = GetTimeMicroSeconds();

            IPAddress ip;
            IPAddress.TryParse(pIP, out ip);
            RecieveManager = new Network.RecieveManager(!pJoin, GameID, CurrentLevel, Competitor, this);
            NetManager = new NetworkManager(ip, RecieveManager);

            if (pJoin)
            {
                NetManager.Send("000");
                PlayerID = 0;
                Full = true;
            }
        }

        public void Render()
        {
            GraphicsState gs = Entity.Context.Save();
            Entity.Context.TranslateTransform(-XScroll + 100, 0);
            CurrentLevel.Render();
            Player.Render();
            Competitor.Render();
            Entity.Context.Restore(gs);
        }

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

                if (Full)
                    NetManager.Send("010 " + GameID + " " + PlayerID + " " + Player.Position.X + "-" + Player.Position.Y);

                if (netCooldown % 6 == 0)
                {
                    Debug.WriteLine("1");
                    netCooldown = 0;
                }

                CapLoop(Constants.UPDATE_CAP * Constants.GAME_SPEED, stopWatchUp.ElapsedMilliseconds - lastUp);
                lastUp = stopWatchUp.ElapsedMilliseconds;
            }

            NetManager.StopListening();
        }

        public static void CapLoop(float pCap, long pExecutionTime)
        {
            float Millis = (1000f / (float)pCap);
            int sleepTime = (int)(Millis - pExecutionTime);
            if (sleepTime > 0)
                Thread.Sleep(sleepTime);
        }

        public static long GetTimeMicroSeconds()
        {
            return Convert.ToInt64(DateTime.Now.ToString("HHmmssffffff"));
        }

        public static Level CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }

        public Character Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public int PlayerID
        {
            get { return _playerID; }
            set { _playerID = value; }
        }

        public Character Competitor
        {
            get { return _competitor; }
            set { _competitor = value; }
        }

        public long GameID
        {
            get { return _gameID; }
            set { _gameID = value; }
        }

        public bool Running
        {
            get { return _running; }
            set { _running = value; }
        }

        public NetworkManager NetManager
        {
            get { return _netManager; }
            set { _netManager = value; }
        }

        public RecieveManager RecieveManager
        {
            get { return _recieveManager; }
            set { _recieveManager = value; }
        }
    }
}
