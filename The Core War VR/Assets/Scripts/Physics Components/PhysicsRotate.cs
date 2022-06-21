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


    Vector3 rotateAxis;

    private Rigidbody body;

    private float rotationValue = 0;

    public override void OnStartClient()
    {
        base.OnStartClient();

        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Call the ServerRPC
        RotateBody();
    }

    [ServerRpc (RequireOwnership = false)]
    private void RotateBody()
    {
        // Read the input
        rotationValue = rotationInput.GetValue();
        if (rotationValue != 0)
        {
            rotateAxis = transform.up * rotationAxis.y + transform.forward * rotationAxis.z + transform.right * rotationAxis.x;
            Debug.Log("Rotating " + gameObject.name);
            body.angularVelocity = rotateAxis * rotationValue * rotationSpeed;
        } else
        {
            body.angularVelocity = Vector3.zero;
        }
    }
}
