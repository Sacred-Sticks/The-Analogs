using FishNet.Object;
using UnityEngine;

public class DisableColliderIfOwner : NetworkBehaviour
{
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        GetComponent<Collider>().enabled = false;
    }
}
