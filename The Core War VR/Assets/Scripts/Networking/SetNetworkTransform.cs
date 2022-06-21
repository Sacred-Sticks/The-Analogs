using FishNet.Component.Transforming;
using FishNet.Object;
using System.Linq;
using UnityEngine;
using UnityEngine.SpatialTracking;

[RequireComponent(typeof(NetworkTransform))]
public class SetNetworkTransform : NetworkBehaviour
{
    [SerializeField] private string handName;
    [Space]
    [SerializeField] private int boneIndex;
    [SerializeField] private int boneChildCount;
    [SerializeField] private Transform transformCopy;

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Verify that this only happens for the client playing
        if (!IsOwner) return;

        // Find the pivot
        Transform hand = GameObject.Find(handName).transform;
        Transform pivot = hand.GetChild(5);

        // Finalize bones for Pivot (-1) and Hand (-2)
        if (boneIndex == -1)
        {
            transformCopy = pivot;
            return;
        }

        if (boneIndex == -2)
        {
            transformCopy = hand;
            return;
        }

        // Find the appropriate bones
        Transform child = pivot.GetChild(boneIndex);
        for (int i = 0; i < boneChildCount; i++)
        {
            child = child.GetChild(0);
        }

        // Assign the transformCopy and disable the colliders
        transformCopy = child;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void Update()
    {
        // Verify that the client owns this object
        if (!IsOwner) return;

        transform.position = transformCopy.position;
        transform.rotation = transformCopy.rotation;
    }
}
