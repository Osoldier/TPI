/**
 * Document: Keyboard.cs
 * Description: Permet de gérer les entrées du  clavier
 * Auteur: Inconnu
 * Source: http://stackoverflow.com/questions/27098814/check-if-non-modifier-keys-are-in-pressed-state-during-mouse-events/27098952#27098952
 * Date: 29.05.15
 * Version: 0.1
 */
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TPI.Engine
{
    /// <summary>
    /// Permet de gérer les entrées du clavier à un niveau plus bas que Forms
    /// </summary>
    public static class Keyboard
    {
        /// <summary>
        /// Liste des états possibles pour une touche
        /// </summary>
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        /// <summary>
        /// Demande à windows l’état d’une touche via la dll user32
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        /// <summary>
        /// Donne l’état d’une touche
        /// </summary>
        /// <param name="key">Touche à demander</param>
        /// <returns>KeyStates</returns>
        private static KeyStates GetKeyState(Keys key)
        {
            KeyStates state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            //If the high-order bit is 1, the key is down
            //otherwise, it is up.
            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            //If the low-order bit is 1, the key is toggled.
            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }

        /// <summary>
        /// Demande si une touche est enfoncée
        /// </summary>
        /// <param name="key">Touche à demander</param>
        /// <returns>true si la touche donnée en paramètre est enfoncée, false sinon</returns>
        public static bool IsKeyDown(Keys key)
        {
            return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }

        /// <summary>
        /// Demande is une touche est basculée
        /// </summary>
        /// <param name="key">Touche à demander</param>
        /// <returns>true si la touche donnée en paramètre est basculée, false sinon</returns>
        public static bool IsKeyToggled(Keys key)
        {
            return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
        }
    }
}
