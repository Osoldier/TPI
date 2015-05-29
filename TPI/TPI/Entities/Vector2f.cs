/**
 * Document: Vector2f.cs
 * Description: Vecteur en deux dimentions
 * Auteur: Ibanez Thomas
 * Date: 28.05.15
 * Version: 0.1
 */
namespace TPI.Entities
{
    public class Vector2f
    {
        private float _x, _y;

        public Vector2f(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2f():this(0,0)
        {

        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
    }
}
