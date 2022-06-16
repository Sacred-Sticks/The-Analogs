using FishNet.Component.Transforming;
using FishNet.Object;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform))]
public class TransferOwnership : NetworkBehaviour
{
    NetworkTransform objectData;

    public override void OnStartClient()
    {
        base.OnStartClient();

        objectData = GetComponent<NetworkTransform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner) return;

        NetworkTransform collisionNetworkData = collision.gameObject.GetComponent<NetworkTransform>();

        if (collisionNetworkData != null) ChangeOwnership(collisionNetworkData);
    }

    [ServerRpc]
    private void ChangeOwnership(NetworkTransform collisionData)
    {
        if (collisionData.Owner != objectData.Owner)
        {
            collisionData.GiveOwnership(objectData.Owner);
            Debug.Log(collisionData.gameObject.name + " is now owned by " + objectData.gameObject.name);
        }
    }
}
