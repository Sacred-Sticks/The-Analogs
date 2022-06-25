using Autohand;
using FishNet.Object;
using FishNet.Component.Transforming;
using UnityEngine;

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

    public override void OnStartClient()
    {
        base.OnStartClient();

        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Call the ServerRPC
        MoveBody();
    }

    [ServerRpc (RequireOwnership = false)]
    private void MoveBody()
    {
        // Get inputs for each direction
        if (forwardInput != null) forward = forwardInput.GetValue();
        else forward = 0;
        if (rightwardInput != null) rightward = rightwardInput.GetValue();
        else rightward = 0;
        if (upwardInput != null) upward = upwardInput.GetValue();
        else upward = 0;

        // Combine the inputs into a vector3 to be read relative to the transform rotation
        if (body != null)
        {
            moveToward = (transform.forward * forward + transform.right * rightward + transform.up * upward).normalized * movementSpeed;
            if (moveToward.magnitude != 0) Debug.Log("Moving " + gameObject.name);
            body.velocity = moveToward;
        }
    }
}
