using UnityEngine;


public enum PartsStates
{
    Damaged,
    Good,
    Danger
}

public abstract class MotorPart : MonoBehaviour
{
    public string PartName;

    public PartsStates currentState;

    public virtual void PartDamaged()
    {
        Debug.Log($"{this.name} is damaged.");
    }

    public virtual void PartRepaired()
    {
        Debug.Log($"{this.name} is repaired.");
    }
}
