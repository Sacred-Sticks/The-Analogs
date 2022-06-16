using Autohand;
using FishNet.Object;
using FishNet.Component.Transforming;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class PhysicsRotate : NetworkBehaviour
{
    [SerializeField] private PhysicsGadgetConfigurableLimitReader rotationInput;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float rotationSpeed;

    private Rigidbody body;

    private float rotationValue = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsOwner)
            RotateBody();
    }

    [ServerRpc]
    private void RotateBody()
    {
        Debug.Log("Rotating " + gameObject.name);
        rotationValue = rotationInput.GetValue();
        Vector3 rotateAxis = transform.up * rotationAxis.y + transform.forward * rotationAxis.z + transform.right * rotationAxis.x;
        body.angularVelocity = rotateAxis * rotationValue * rotationSpeed;
    }
}
