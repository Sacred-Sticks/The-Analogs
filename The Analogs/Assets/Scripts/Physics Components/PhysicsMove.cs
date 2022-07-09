using FishNet.Object;
using FishNet.Component.Transforming;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class PhysicsMove : NetworkBehaviour
{
    [SerializeField] private PhysicsInputSystem[] forwardInput;
    [SerializeField] private PhysicsInputSystem[] rightwardInput;
    [SerializeField] private PhysicsInputSystem[] upwardInput;
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

    private void FixedUpdate()
    {
        if (body == null) return;

        // Call the ServerRPC
        MoveBody();
    }

    [ServerRpc(RequireOwnership = false)]
    private void MoveBody()
    {
        // Get inputs for each direction
        forward = 0;
        rightward = 0;
        upward = 0;
        if (forwardInput != null)
        {
            for (int i = 0; i < forwardInput.Length; i++) forward += forwardInput[i].GetValue();
        }
        if (rightwardInput != null)
        {
            for (int i = 0; i < rightwardInput.Length; i++) rightward += rightwardInput[i].GetValue();
        }
        if (upwardInput != null)
        {
            for (int i = 0; i < upwardInput.Length; i++) upward += upwardInput[i].GetValue();
        }

        // Combine the inputs into a vector3 to be read relative to the transform rotation
        moveToward = (transform.forward * forward + transform.right * rightward + transform.up * upward).normalized * movementSpeed;
        //Debug.Log("Moving " + gameObject.name + " with velocity " + moveToward);
        body.velocity = moveToward;
    }
}
