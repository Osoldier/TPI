/**
 * Document: FrmGame.cs
 * Description: Fenêtre de jeu
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System.Threading;
using System.Windows.Forms;
using TPI.Engine;

namespace TPI
{
    public partial class FrmGame : Form
    {
        private Game _game;
        private Thread thrUpdate;

        public FrmGame(bool pJoin, string pIP, string pName)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Game = new Game(pJoin, pIP, pName);
            thrUpdate = new Thread(new ThreadStart(Game.Update));
            thrUpdate.Start();
        }

        ~FrmGame()
        {
            Game.Running = false;
            thrUpdate.Abort();
        }

        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            Entity.Context = e.Graphics;
            Game.Render();
            this.Invalidate();
        }

        private void FrmGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Game.Running = false;
            thrUpdate.Join();
        }

        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }
    }
}
