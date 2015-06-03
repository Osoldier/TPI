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
    /// <summary>
    /// Plateforme
    /// </summary>
    public class Platform : Block
    {
        /// <summary>Couleur des plateformes</summary>
        private readonly Color COLOR = Color.FromArgb(26, 235, 178);
        private bool _end = false;

        /// <summary>
        /// Défini la position et la taille de la plateforme, 
        /// transmet la couleur depuis la constante.
        /// </summary>
        /// <param name="pPosition">Position de la plateforme</param>
        /// <param name="pSize">taille de la plateforme</param>
        public Platform(Vector2f pPosition, Vector2f pSize)
        {
            this.Position = pPosition;
            this.Size = pSize;
            this.Color = COLOR;
        }

        /// <summary>
        /// Implémentation de la méthode update d’Entity, pour l’instant vide
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>La plateforme est la dernière du niveau ?</summary>
        public bool End
        {
            get { return _end; }
            set { _end = value; }
        }
    }
}
