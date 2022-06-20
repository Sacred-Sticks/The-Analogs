using UnityEngine;
using FishNet.Object;

public class DisableVisualsIfOwner : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (IsOwner)
        GetComponent<Renderer>().enabled = false;
    }
}
