using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsFollow : NetworkBehaviour
{
    [SerializeField] private string leftControllerStr;
    [SerializeField] private string rightControllerStr;
    [SerializeField] private string cameraStr;
    [Space]
    [SerializeField] private float followVelocityMultiplier;
    [SerializeField] private float angularVelocityMultiplier;
    [Space]
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    private Transform leftController;
    private Transform rightController;
    private Transform cameraTransform;
    private Rigidbody leftBody;
    private Rigidbody rightBody;
    private Rigidbody body;

    public override void OnStartClient()
    {
        base.OnStartClient();

        leftController = GameObject.Find(leftControllerStr).transform;
        leftBody = leftHand.GetComponent<Rigidbody>();
        rightController = GameObject.Find(rightControllerStr).transform;
        rightBody = rightHand.GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;

        // Move Head and Hands to match the positions and rotations of the Camera and Controllers
        MoveBody(body, cameraTransform, transform);
        MoveBody(leftBody, leftController, leftHand);
        MoveBody(rightBody, rightController, rightHand);
    }

    private void MoveBody(Rigidbody body, Transform goal, Transform currentTransform)
    {
        // Position
        body.velocity = (goal.position - currentTransform.position).normalized * followVelocityMultiplier * Vector3.Distance(currentTransform.position, goal.position);

        // Rotation
        var q = goal.rotation * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * angularVelocityMultiplier * Mathf.Deg2Rad);
    }
}
