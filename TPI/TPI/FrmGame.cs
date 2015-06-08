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
using System.Diagnostics;

namespace TPI
{
    /// <summary>
    /// Fenêtre servant de canevas pour le jeu,
    /// lors de son initialisation elle passe les paramètres choisis dans le menu 
    /// à la classe principale « Game ».
    /// Elle contient également la méthode et partir de laquelle tout le processus de rendu est lancé en boucle.
    /// </summary>
    public partial class FrmGame : Form
    {

        /// <summary>Instance actuelle du jeu</summary>
        private Game _game;
        /// <summary>Thread de mise à jour</summary>
        private Thread thrUpdate;


        /// <summary>
        /// Active le double buffer, instancie une partie, et lance le thread de mise à jour
        /// </summary>
        /// <param name="pJoin">True si le joueur rejoint une partie, false si il la crée</param>
        /// <param name="pIP">adresse ip de la partie</param>
        /// <param name="pName">Pseudo du joueur</param>
        public FrmGame(bool pJoin, string pIP, string pName)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Game = new Game(pJoin, pIP, pName, this);
            thrUpdate = new Thread(new ThreadStart(Game.Update));
            thrUpdate.Start();
        }

        /// <summary>
        /// Arrête le thread de mise à jour
        /// </summary>
        ~FrmGame()
        {
            Game.Running = false;
            thrUpdate.Abort();
        }

        private Stopwatch stopWatch = Stopwatch.StartNew();
        long lastUp = 0;
        /// <summary>
        /// Met à jour le contexte de rendu, lance le rendu et invalide la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmGame_Paint(object sender, PaintEventArgs e)
        {
            Entity.Context = e.Graphics;
            Game.Render();
            this.Invalidate();
            Game.CapLoop(60, stopWatch.ElapsedMilliseconds - lastUp);
            lastUp = stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Arrête le thread de mise à jour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
