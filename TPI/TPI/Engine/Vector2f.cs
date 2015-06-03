/**
 * Document: Vector2f.cs
 * Description: Vecteur en deux dimentions
 * Auteur: Ibanez Thomas
 * Date: 28.05.15
 * Version: 0.1
 */
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
        public Vector2f():this(0,0)
        {

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
