/**
 * Document: NetworkRecieveCallback.cs
 * Description: Utiliser pour gérer la réception des messages réseau
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
namespace TPI.Network
{
    /// <summary>
    /// Interface NetworkRecieveCallback
    /// </summary>
    public interface NetworkRecieveCallback
    {
        /// <summary>
        /// Appellé quand un message est reçu.
        /// </summary>
        /// <param name="data">Le message.</param>
        void OnRecieve(string data);
    }
}
