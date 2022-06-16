using FishNet.Object;
using UnityEngine;

public class DisableVisualsIfOwner : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;
        GetComponent<Renderer>().enabled = false;
    }
}
