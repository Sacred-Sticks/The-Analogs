using FishNet.Component.Transforming;
using FishNet.Object;
using UnityEngine;

public class TransferOwnership : NetworkBehaviour
{
    [SerializeField] NetworkObject objectData;

    private void OnTriggerEnter(Collider coll)
    {
        // Verify Client Owns the GameObject
        if (!IsOwner) return;

        // Get Network Data for the collided object and send to the serverRPC
        NetworkObject collisionNetworkData = coll.gameObject.GetComponent<NetworkObject>();
        if (collisionNetworkData != null) ServerChangeOwnership(collisionNetworkData);
    }

    [ServerRpc]
    private void ServerChangeOwnership(NetworkObject collisionData)
    {
        // Check to make sure the collided object doesn't already share the same owner
        if (collisionData.OwnerId == objectData.OwnerId) return;

        // Set ownership of the collided network object to the owner of this network object
        collisionData.GiveOwnership(objectData.Owner);
    }
}
