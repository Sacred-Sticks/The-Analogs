using FishNet.Component.Transforming;
using FishNet.Object;
using UnityEngine;

public class TransferOwnership : NetworkBehaviour
{
    NetworkObject objectData;

    public override void OnStartClient()
    {
        base.OnStartClient();

        objectData = GetComponent<NetworkObject>();
        if (objectData == null) Debug.Log("Object Data not found");
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger Entered");

        if (!IsOwner) return;

        Debug.Log("Owned Object");

        NetworkObject collisionNetworkData = collision.gameObject.GetComponent<NetworkObject>();

        if (collisionNetworkData != null) ServerChangeOwnership(collisionNetworkData);
    }

    [ServerRpc]
    private void ServerChangeOwnership(NetworkObject collisionData)
    {
        Debug.Log("Collision Data Found");
        if (collisionData.Owner != objectData.Owner)
        {
            collisionData.GiveOwnership(objectData.Owner);
            Debug.Log(collisionData.gameObject.name + " is now owned by " + collisionData.gameObject.name);
        }
    }
}
