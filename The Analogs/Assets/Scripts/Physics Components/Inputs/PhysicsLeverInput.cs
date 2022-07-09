using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class PhysicsLeverInput : PhysicsInputSystem
{
    [SerializeField] private float threshold;
    [SerializeField] private float deadzone;

    private HingeJoint joint;
    private Rigidbody body;
    private float value;
    private float initialXRot;

    private float lowerLimit;
    private float upperLimit;
    private float range;

    private void Awake()
    {
        joint = GetComponent<HingeJoint>();
        body = GetComponent<Rigidbody>();
        initialXRot = transform.localRotation.eulerAngles.x;
        lowerLimit = joint.limits.min;
        upperLimit = joint.limits.max;
        range = (upperLimit - lowerLimit) / 2;
    }

    private void FixedUpdate()
    {
        SetValue();
        RoundValue();
        Debug.Log(transform.parent.name + " has a value of " + value + " after rounding");
    }

    public override void SetValue()
    {
        float temp = transform.localRotation.eulerAngles.x;
        if (temp > upperLimit + 1) temp -= 360;

        value = (initialXRot + temp) / range;

        value = Mathf.Clamp(value, -1f, 1f);
    }

    public override void RoundValue()
    {
        if (value < deadzone && value > -deadzone)
        {
            value = 0;
        }
        else if (value > threshold) value = 1;
        else if (value < -threshold) value = -1;
    }

    public override float GetValue()
    {
        return value;
    }
}
