/**
 * Document: Character.cs
 * Description: Personnage
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TPI.Engine;

namespace TPI.Entities
{
    public class Character : Entity
    {
        private const float WIDTH = 30;
        private const float HEIGHT = 30;
        private const float SPEED = 5f;

        private float _verticalSpeed = 0;
        private float _horizontalSpeed = 0;

        private bool _grounded = false;

        private float t = 0;

        private bool _jumping = false;
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

            VerticalSpeed = gravityVelocity;

            if (!this.wouldCollide(Game.CurrentLevel.Elements, 0, -gravityVelocity) && !this.isCollided(Game.CurrentLevel.Elements))
            {
                t += 1.0f / (float)Constants.UPDATE_CAP;
                this.Grounded = false;
            }
            else
            {
                VerticalSpeed -= gravityVelocity;
                this.Grounded = true;
                Jumping = false;
                this.Position.Y = this.LastCollidable.Position.Y - this.Size.Y;
                t = 1;
            }

            if (Jumping)
            {
                VerticalSpeed += (Constants.JUMP_SPEED * t);
            }

            this.Position.Y -= VerticalSpeed;
            bool KEY_LEFT = Keyboard.IsKeyDown(Keys.A) || Keyboard.IsKeyDown(Keys.Left);
            bool KEY_RIGHT = Keyboard.IsKeyDown(Keys.D) || Keyboard.IsKeyDown(Keys.Right);
            bool KEY_UP = Keyboard.IsKeyDown(Keys.Space) || Keyboard.IsKeyDown(Keys.W) || Keyboard.IsKeyDown(Keys.Up);

            if (KEY_LEFT && !wouldCollide(Game.CurrentLevel.Elements, -SPEED, -1))
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

        public bool Jumping
        {
            get { return _jumping; }
            set { _jumping = value; }
        }

        public float HorizontalSpeed
        {
            get { return _horizontalSpeed; }
            set { _horizontalSpeed = value; }
        }
    }
}
