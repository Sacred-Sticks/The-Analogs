using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public sealed class PhysicsButtonInput : PhysicsInputSystem
{
    [SerializeField] private float threshold;
    [SerializeField] private float deadzone;
    [SerializeField] private float multiplier;

    private ConfigurableJoint joint;
    private float value;
    private float initialYPos;
    private float limit;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        initialYPos = transform.localPosition.y;
        limit = joint.linearLimit.limit;
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y > 0) transform.localPosition = Vector3.zero;
        SetValue();
        RoundValue();
        value *= multiplier;
        //Debug.Log(transform.parent.name + " value is " + value + " after rounding");
    }

    public override void SetValue()
    {
        value = Mathf.Abs(initialYPos - transform.localPosition.y) / limit;

        value = Mathf.Clamp(value, -1f, 1f);
    }

    public override void RoundValue()
    {
        if (value < deadzone) value = 0;
        else if (value > threshold) value = 1;
    }

    public override float GetValue()
    {
        return value;
    }
}
