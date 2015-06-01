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
using System.Threading;
using TPI.Engine;
using TPI.Entities;

namespace TPI
{
    public class Game
    {
        private static Level _currentLevel;
        private Character _player, _competitor;
        private bool _running;
        private float XScroll = 0f;

        public Game(bool pJoin, string pIP, string pName)
        {
            if (!pJoin)
            {
                CurrentLevel = new Level(-1);
            }
            Running = true;
            Player = new Character(Color.Blue, new Vector2f());
            Competitor = new Character(Color.Red, new Vector2f());
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

            while (Running)
            {
                CurrentLevel.Update();
                Player.Update();
                this.XScroll = Player.Position.X;
                CapLoop(Constants.UPDATE_CAP, stopWatchUp.ElapsedMilliseconds - lastUp);
                lastUp = stopWatchUp.ElapsedMilliseconds;
            }
        }

        public static void CapLoop(int pCap, long pExecutionTime)
        {
            float Millis = (1000f / (float)pCap);
            int sleepTime = (int)(Millis - pExecutionTime);
            if (sleepTime > 0)
                Thread.Sleep(sleepTime);
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

        public Character Competitor
        {
            get { return _competitor; }
            set { _competitor = value; }
        }

        public bool Running
        {
            get { return _running; }
            set { _running = value; }
        }
    }
}
