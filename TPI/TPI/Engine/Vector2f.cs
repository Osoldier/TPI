/**
 * Document: Vector2f.cs
 * Description: Vecteur en deux dimentions
 * Auteur: Ibanez Thomas
 * Date: 28.05.15
 * Version: 0.1
 */
using System;
namespace TPI.Engine
{
    /// <summary>
    /// Vecteur en deux dimensions
    /// </summary>
    public class Vector2f
    {
        private float _x, _y;

        /// <summary>
        /// Création d’un vecteur avec les composant x et y
        /// </summary>
        /// <param name="x">composant sur l’axe x du vecteur</param>
        /// <param name="y">composant sur l’axe y du vecteur</param>
        public Vector2f(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Création du vecteur 0
        /// </summary>
        public Vector2f()
            : this(0, 0)
        {

        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public void Normalize()
        {
            float mag = this.Magnitude();
            this.X /= mag;
            this.Y /= mag;
        }

        public double GetAngle()
        {
            return (180 / Math.PI) * (Math.Atan2(this.X, this.Y));
        }

        public static Vector2f operator *(Vector2f pSource, float pScale)
        {
            return new Vector2f(pSource.X * pScale, pSource.Y * pScale);
        }

        public static Vector2f operator +(Vector2f pSource, Vector2f pScale)
        {
            return new Vector2f(pSource.X + pScale.X, pSource.Y + pScale.Y);
        }

        public static Vector2f operator -(Vector2f pSource, Vector2f pScale)
        {
            return new Vector2f(pSource.X - pScale.X, pSource.Y - pScale.Y);
        }

        /// <summary>
        /// composant sur l’axe y du vecteur
        /// </summary>
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// composant sur l’axe x du vecteur
        /// </summary>
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
    }
}
