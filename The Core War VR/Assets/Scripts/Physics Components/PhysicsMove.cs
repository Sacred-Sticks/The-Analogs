using Autohand;
using FishNet.Object;
using FishNet.Component.Transforming;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class PhysicsMove : NetworkBehaviour
{
    [SerializeField] private PhysicsGadgetConfigurableLimitReader forwardInput;
    [SerializeField] private PhysicsGadgetConfigurableLimitReader rightwardInput;
    [SerializeField] private PhysicsGadgetConfigurableLimitReader upwardInput;
    [SerializeField] private float movementSpeed;

    private Rigidbody body;

    private Vector3 moveToward;
    float forward = 0;
    float rightward = 0;
    float upward = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsOwner)
            MoveBody();
    }

    [ServerRpc]
    private void MoveBody()
    {
        Debug.Log("Moving " + gameObject.name);
        if (forwardInput != null) forward = forwardInput.GetValue();
        else forward = 0;
        if (rightwardInput != null) rightward = rightwardInput.GetValue();
        else rightward = 0;
        if (upwardInput != null) upward = upwardInput.GetValue();
        else upward = 0;

        moveToward = (transform.forward * forward + transform.right * rightward + transform.up * upward).normalized * movementSpeed;
        body.velocity = moveToward;
    }
}
