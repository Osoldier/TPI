/**
 * Document: Block.cs
 * Description: Classe mère de tous les blocks du jeu
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
using TPI.Engine;

namespace TPI.Entities
{
    /// <summary>
    /// Classe mère de tous les blocks du jeu
    /// </summary>
    public abstract class Block : Entity
    {
        private bool _end = false;

        /// <summary>La plateforme est la dernière du niveau ?</summary>
        public bool End
        {
            get { return _end; }
            set { _end = value; }
        }
    }
}
