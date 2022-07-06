using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public sealed class PhysicsButtonInput : PhysicsInputSystem
{
    ConfigurableJoint joint;
    float value;
    float initialYPos;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        initialYPos = transform.localPosition.y;
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y > 0) transform.localPosition = Vector3.zero;
        SetValue();
        RoundValue();
        //Debug.Log(transform.parent.name + " value is " + value);
    }

    public override void SetValue()
    {
        value = Mathf.Abs(initialYPos - transform.localPosition.y) / joint.linearLimit.limit;

        value = Mathf.Clamp(value, -1f, 1f);
    }

    public override void RoundValue()
    {
        if (value < 0.01f) value = 0;
        if (value > 0.99f) value = 1;
    }

    public override float GetValue()
    {
        return value;
    }
}
