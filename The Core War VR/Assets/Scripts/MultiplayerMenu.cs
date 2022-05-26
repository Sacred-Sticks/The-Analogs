using UnityEngine;
using UnityEngine.UI;
using FishNet;

public sealed class MultiplayerMenu : MonoBehaviour
{
    [SerializeField] private Button host;
    [SerializeField] private Button connect;

    private void Start()
    {
        host.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        });

        connect.onClick.AddListener(() =>
        {
            InstanceFinder.ClientManager.StartConnection();
        });
    }
}
