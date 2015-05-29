/**
 * Document: Platform.cs
 * Description: Plateforme
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using TPI.Engine;
using System.Drawing;

namespace TPI.Entities
{
    public class Platform : Block
    {
        private readonly Color COLOR = Color.FromArgb(26, 235, 178);
        public Platform(Vector2f pPosition, Vector2f pSize)
        {
            this.Position = pPosition;
            this.Size = pSize;
            this.Color = COLOR;
        }
        public override void Update()
        {
        }
    }
}
