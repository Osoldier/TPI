/**
 * Document: Entity.cs
 * Description: Classe mère de tout les objets du jeu
 * Auteur: Ibanez Thomas
 * Date: 28.05.15
 * Version: 0.1
 */
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TPI.Engine
{
    public abstract class Entity
    {
        public static Graphics Context;

        private Vector2f _position;
        private Vector2f _size;
        private float _rotation;
        private Image _texture;
        private Pen _pen;
        private Color _color;
        private Entity _lastCollidable;

        public virtual void Render()
        {
            if (Pen == null)
            {
                Pen = new Pen(Color, 0.1f);
            }

            GraphicsState state = Context.Save();
            Context.TranslateTransform(Position.X + Size.X / 2, Position.Y + Size.Y / 2);
            Context.RotateTransform(Rotation);
            Context.TranslateTransform(-Size.X / 2, -Size.Y / 2);
            if (Texture == null)
            {
                Context.DrawRectangle(Pen, 0, 0, Size.X, Size.Y);
            }
            else
            {
                Context.DrawImage(Texture, 0, 0, 1, 1);
            }
            Context.Restore(state);
        }

        public abstract void Update();

        /// <summary>
        /// Teste si il y a une collision cette entitée et une de la liste dérivée d'entités
        /// </summary>
        /// <typeparam name="T">T doit être hérité de Entity</typeparam>
        /// <param name="pCollidables">Liste à tester</param>
        /// <returns>True si il y a une collision false sinon</returns>
        public bool isCollided<T>(List<T> pCollidables) where T : Entity
        {
            foreach (Entity e in pCollidables)
            {
                if (this.isCollided(e))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Teste si il y a une collision entre cette entitée et une autre
        /// </summary>
        /// <typeparam name="T">T doit être hérité de Entity</typeparam>
        /// <param name="pCollidables">Entité à tester</param>
        /// <returns>True si il y a une collision false sinon</returns>
        public bool isCollided<T>(T pCollidable) where T : Entity
        {
            return wouldCollide(pCollidable, 0, 0);
        }

        /// <summary>
        /// Teste si il y aurait une collision entre cette entitée et une de la liste
        /// </summary>
        /// <typeparam name="T">T doit être hérité de Entity</typeparam>
        /// <param name="pCollidables">Liste à tester</param>
        /// <param name="pDeltaX">Déplacement en X de la hitbox</param>
        /// <param name="pDeltaY">Déplacement en Y de la hitbox</param>
        /// <returns>True si il y a une collision false sinon</returns>
        public bool wouldCollide<T>(List<T> pCollidables, float pDeltaX, float pDeltaY) where T : Entity
        {
            foreach (Entity e in pCollidables)
            {
                if (this.wouldCollide(e, pDeltaX, pDeltaY))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Teste si il y aurait une collision cette entitée et une autre
        /// </summary>
        /// <typeparam name="T">T doit être hérité de Entity</typeparam>
        /// <param name="pCollidables"></param>
        /// <param name="pDeltaX"></param>
        /// <param name="pDeltaY"></param>
        /// <returns>True si il y a une collision false sinon</returns>
        public bool wouldCollide<T>(T pCollidable, float pDeltaX, float pDeltaY) where T : Entity
        {
            if (pCollidable.Position.X + pCollidable.Size.X >= this.Position.X + pDeltaX && pCollidable.Position.X <= this.Position.X + this.Size.X + pDeltaX)
            {
                if (pCollidable.Position.Y + pCollidable.Size.Y >= this.Position.Y + pDeltaY && pCollidable.Position.Y <= this.Position.Y + this.Size.Y + pDeltaY)
                {
                    LastCollidable = pCollidable;
                    return true;
                }
            }
            return false;
        }

        public Image Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }


        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Vector2f Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public Vector2f Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Pen Pen
        {
            get { return _pen; }
            set { _pen = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public Entity LastCollidable
        {
            get { return _lastCollidable; }
            set { _lastCollidable = value; }
        }
    }
}
