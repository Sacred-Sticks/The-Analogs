using UnityEngine;
using FishNet.Object;

public class DisableVisualsIfOwner : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        GetComponent<Renderer>().enabled = false;
    }
}
