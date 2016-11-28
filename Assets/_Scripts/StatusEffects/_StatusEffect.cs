using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _StatusEffect{
    protected string name;
    protected GameObject target;
    protected bool destroy = false;

    public void TryApply()
    {
        if(CanApplyEffect())
        {
            ApplyEffect();
        }
    }

    protected abstract void ApplyEffect();

    protected abstract bool CanApplyEffect();

    public virtual float MovementScale()
    {
        return 1.0F;
    }

    public override string ToString()
    {
        return name;
    }

    protected void RemoveEffect()
    {
        target.GetComponent<EnemyBehaviour>().RemoveEffectByName(ToString());
    }
}