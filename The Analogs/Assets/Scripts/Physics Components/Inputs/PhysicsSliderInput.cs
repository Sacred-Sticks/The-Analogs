using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSliderInput : PhysicsInputSystem
{
    [SerializeField] private float threshold;
    [SerializeField] private float deadzone;

    private ConfigurableJoint joint;

    private float value;
    private float range;
    private float initialXPos;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        range = joint.linearLimit.limit / 2;
        initialXPos = transform.localPosition.x;
    }

    private void FixedUpdate()
    {
        SetValue();
        RoundValue();
        Debug.Log(transform.parent.name + " has a value of " + value + " after rounding");
    }

    public override void SetValue()
    {
        value = (initialXPos + transform.localPosition.x) / range;
    }

    public override void RoundValue()
    {
        if (Mathf.Abs(value) < deadzone) value = 0;
        else if (value > threshold) value = 1;
        else if (value < -threshold) value = -1;
    }

    public override float GetValue()
    {
        return value;
    }
}
