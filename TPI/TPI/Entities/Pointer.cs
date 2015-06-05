using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
/**
 * Document: Pointer.cs
 * Description: Pointeur permettant de visualiser l'adversaire
 * Auteur: Ibanez Thomas
 * Date: 03.05.15
 * Version: 0.1
 */
using TPI.Engine;

namespace TPI.Entities
{
    /// <summary>
    /// Pointeur permettant de visualiser l'adversaire
    /// </summary>
    public class Pointer : Entity
    {
        private const float LENGHT = 80;

        private Character _spying, _origin;
        private bool _renderable;
        private Vector2f _target;

        /// <summary>
        /// Initialise le pointeur
        /// </summary>
        public Pointer(Character pSpying, Character pOrigin)
        {
            this.Spying = pSpying;
            this.Origin = pOrigin;
            this.Position = new Vector2f();
            this.Pen = new Pen(Color.Red, 5);
            this.Target = new Vector2f();
        }

        /// <summary>
        /// Affiche le pointeur
        /// </summary>
        public override void Render()
        {
            if (Renderable)
            {
                GraphicsState gs = Entity.Context.Save();
                Entity.Context.ResetTransform();
                Pen.EndCap = LineCap.ArrowAnchor;
                Entity.Context.DrawLine(this.Pen, new PointF(this.Position.X, this.Position.Y), new PointF(this.Target.X, this.Target.Y));
                Entity.Context.Restore(gs);
            }
        }

        /// <summary>
        /// Met a jour le pointeur
        /// </summary>
        public override void Update()
        {
            Renderable = false;
            this.Position.Y = this.Origin.Position.Y;
            if (this.Spying.Position.X + this.Spying.Size.X < this.Origin.Position.X - 100)
            {
                Renderable = true;
                this.Position.X = LENGHT;
                this.Target.X = 30;
                this.Target.Y = this.Position.Y = 720 / 2;
            } else if (this.Spying.Position.X > this.Origin.Position.X - 100 + 1280) {
                Renderable = true;
                this.Position.X = 1280 - LENGHT;
                this.Target.X = 1250;
                this.Target.Y = this.Position.Y = 720 / 2;
            }
        }

        public Character Spying
        {
            get { return _spying; }
            set { _spying = value; }
        }

        public bool Renderable
        {
            get { return _renderable; }
            set { _renderable = value; }
        }

        public Character Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public Vector2f Target
        {
            get { return _target; }
            set { _target = value; }
        }
    }
}
