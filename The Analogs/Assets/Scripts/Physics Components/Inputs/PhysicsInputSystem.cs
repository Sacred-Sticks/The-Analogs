using UnityEngine;

public abstract class PhysicsInputSystem : MonoBehaviour
{
    public abstract void SetValue();
    public abstract void RoundValue();
    public abstract float GetValue();
}
