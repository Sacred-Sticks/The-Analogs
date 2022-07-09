using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPulleyInput : PhysicsInputSystem
{
    [SerializeField] private float threshold;
    [SerializeField] private float deadzone;
    [SerializeField] private float multiplier;

    private ConfigurableJoint joint;
    private Vector3 initialPos;

    private float limit;
    private float value;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        initialPos = transform.localPosition;
        limit = joint.linearLimit.limit;
    }

    private void FixedUpdate()
    {
        //if (transform.localPosition.y > 0) transform.localPosition = Vector3.zero;
        SetValue();
        RoundValue();
        value *= multiplier;
        //Debug.Log(transform.parent.name + " has a value of " + value + " after rounding");
    }

    public override void SetValue()
    {
        value = Mathf.Abs(Vector3.Distance(initialPos, transform.localPosition)) / limit;
    }

    public override void RoundValue()
    {
        if (value < deadzone) value = 0;
        if (value > threshold) value = 1;
    }

    public override float GetValue()
    {
        return value;
    }
}
