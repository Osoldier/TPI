/**
 * Document: NetworkRecieveCallback.cs
 * Description: Utiliser pour gérer la réception des messages réseau
 * Auteur: Ibanez Thomas
 * Date: 29.05.15
 * Version: 0.1
 */
namespace TPI.Network
{
    public interface NetworkRecieveCallback
    {
        void OnRecieve(byte[] data);
    }
}
