using UnityEngine;
using FishNet.Object;

public class DisableColliderIfOwner : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        GetComponent<Collider>().enabled = false;
    }
}
