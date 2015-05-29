/**
 * Document: Character.cs
 * Description: Personnage
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using TPI.Engine;
using System.Drawing;
using System;

namespace TPI.Entities
{
    public class Character : Entity
    {
        private const float WIDTH = 30;
        private const float HEIGHT = 30;

        private float _verticalSpeed = 0;
        private bool _grounded = false;


        private float t = 0;

        private bool _isJumping = false;
        private bool _alive = true;

        public Character(Color pColor, Vector2f pPosition)
        {
            this.Size = new Vector2f(WIDTH, HEIGHT);
            this.Color = pColor;
            this.Position = pPosition;
        }

        public override void Update()
        {
            float gravityVelocity = (float)(-Constants.g * Math.Pow((t), 2)) / 2;
            this.Position.Y -= gravityVelocity;
            if(!this.wouldCollide(Game.CurrentLevel.Elements, 0, gravityVelocity)) {
                t += 1.0f / (float)Constants.UPDATE_CAP;
            }
            else
            {
                this.Position.Y += gravityVelocity;
                this.Position.Y = this.LastCollidable.Position.Y-this.Size.Y;
                t = 0;
            }
            
        }

        public float VerticalSpeed
        {
            get { return _verticalSpeed; }
            set { _verticalSpeed = value; }
        }

        public bool Grounded
        {
            get { return _grounded; }
            set { _grounded = value; }
        }

        public bool Alive
        {
            get { return _alive; }
            set { _alive = value; }
        }

        public bool IsJumping
        {
            get { return _isJumping; }
            set { _isJumping = value; }
        }
    }
}
