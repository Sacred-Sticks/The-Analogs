using UnityEngine;
using UnityEngine.UI;
using FishNet;

public sealed class MultiplayerMenu : MonoBehaviour
{
    public void HostConnection()
    {
        InstanceFinder.ServerManager.StartConnection();
        InstanceFinder.ClientManager.StartConnection();
    }

    public void ClientConnection()
    {
        InstanceFinder.ClientManager.StartConnection();
    }
}
