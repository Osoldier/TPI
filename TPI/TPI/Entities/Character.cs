/**
 * Document: Character.cs
 * Description: Personnage du jeu
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TPI.Engine;

namespace TPI.Entities
{
    /// <summary>
    /// Personnage du jeu
    /// </summary>
    public class Character : Entity
    {
        /// <summary>Longueur d’un personnage</summary>
        private const float WIDTH = 30;
        /// <summary>Hauteur d'un personnage</summary>
        private const float HEIGHT = 30;
        /// <summary>Vitesse horizontale d'un personnage</summary>
        private const float SPEED = 5f;

        private float _verticalSpeed = 0;

        private bool _grounded = false;

        private float t = 0;

        private bool _jumping = false;
        private bool _alive = true;

        /// <summary>
        /// Crée le personnage selon sa position et sa couleur. 
        /// La taille est récupérée dans les constantes
        /// </summary>
        /// <param name="pColor">Couleur du personnage</param>
        /// <param name="pPosition">Position du personnage</param>
        public Character(Color pColor, Vector2f pPosition)
        {
            this.Size = new Vector2f(WIDTH, HEIGHT);
            this.Color = pColor;
            this.Position = pPosition;
        }

        /// <summary>
        /// Applique la physique et gère les entrées
        /// </summary>
        public override void Update()
        {
            lock (Level.CollectionLocker)
            {
                if (Game.GameStarted)
                {
                    ApplyPhysic();
                    HandleInput();
                }
            }

            if (Position.Y > 730)
            {
                Position.Y = 0;
                Position.X = 0;
            }
        }

        /// <summary>
        /// Applique la physique
        /// </summary>
        private void ApplyPhysic()
        {
            float gravityVelocity = (float)(-Constants.g * Constants.METER_TO_UNIT * Math.Pow((t), 2)) / 2;

            VerticalSpeed = gravityVelocity;

            if (!this.wouldCollide(Game.CurrentLevel.Elements, 0, -gravityVelocity) && !this.isCollided(Game.CurrentLevel.Elements))
            {
                t += 1.0f / Constants.UPDATE_CAP;
                this.Grounded = false;
            }
            else
            {
                if (LastCollidable.GetType().IsSubclassOf(typeof(Block)))
                {
                    Block b = (Block)LastCollidable;
                    if (b.End)
                    {
                        Game.GameStarted = false;
                        Game.Timer.Stop();
                    }
                }
                VerticalSpeed -= gravityVelocity;
                this.Grounded = true;
                Jumping = false;
                this.Position.Y = this.LastCollidable.Position.Y - this.Size.Y;
                t = 1;
            }

            if (Jumping)
            {
                VerticalSpeed += (Constants.JUMP_SPEED * t) * Constants.METER_TO_UNIT;
            }

            this.Position.Y -= VerticalSpeed;
        }

        /// <summary>
        /// Gère les entrées
        /// </summary>
        private void HandleInput()
        {

            bool KEY_LEFT = Keyboard.IsKeyDown(Keys.A) || Keyboard.IsKeyDown(Keys.Left);
            bool KEY_RIGHT = Keyboard.IsKeyDown(Keys.D) || Keyboard.IsKeyDown(Keys.Right);
            bool KEY_UP = Keyboard.IsKeyDown(Keys.Space) || Keyboard.IsKeyDown(Keys.W) || Keyboard.IsKeyDown(Keys.Up);

            if (KEY_LEFT && !wouldCollide(Game.CurrentLevel.Elements, -SPEED, -1) && this.Position.X >= 5)
            {
                this.Position.X -= SPEED;
            }
            if (KEY_RIGHT && !wouldCollide(Game.CurrentLevel.Elements, SPEED, -1))
            {
                this.Position.X += SPEED;
            }
            if (KEY_UP)
            {
                if (this.Grounded)
                {
                    Jumping = true;
                    this.Position.Y -= 10;
                }
            }
        }

        /// <summary>Vitesse verticale</summary>
        private float VerticalSpeed
        {
            get { return _verticalSpeed; }
            set { _verticalSpeed = value; }
        }

        /// <summary>Le personnage est-t-il posé sur un block</summary>
        private bool Grounded
        {
            get { return _grounded; }
            set { _grounded = value; }
        }

        /// <summary>Le personnage est "vivant"</summary>
        public bool Alive
        {
            get { return _alive; }
            set { _alive = value; }
        }

        /// <summary>variable temps utilisée pour le calcul de la physique</summary>
        public float T
        {
            get { return t; }
            set { t = value; }
        }

        /// <summary>Le personnage est en train de sauter</summary>
        private bool Jumping
        {
            get { return _jumping; }
            set { _jumping = value; }
        }
    }
}
