/**
 * Document: Constants.cs
 * Description: Constantes du jeu
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
namespace TPI.Engine
{
    /// <summary>
    /// Contient toutes les constantes du jeu
    /// </summary>
    public static class Constants
    {
        /// <summary>Accélération gravitationnelle [m/s2]</summary>
        public const float g = 9.81f;
        /// <summary>nombre d’unités dans un mètre [u/m]</summary>
        public const float METER_TO_UNIT = 2f;
        /// <summary>vitesse initiale d’un saut [m/s]</summary>
        public const float JUMP_SPEED = 8f;
        /// <summary>nombre de mise à jour dans une seconde</summary>
        public const int UPDATE_CAP = 60;
        /// <summary>Vitesse du jeu, 1 = normal, &lt;1 = lent, &gt;1 = rapide</summary>
        public const float GAME_SPEED = 1f;
        ///<summary>Une minute en millisecondes</summary>
        public const int BASE_TIME = 60000;
    }
}
