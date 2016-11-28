using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _StatusEffect{
    protected string name;
    protected GameObject target;
    protected bool destroy = false;
    protected float lastPushTime;
    protected float decayTime; //How long it takes until the effect expires.

    public _StatusEffect(string name, GameObject target, float decTime)
    {
        lastPushTime = Time.time;
        this.target = target;
        this.name = name;
        this.decayTime = decTime;
        target.GetComponent<EnemyBehaviour>().AddStatusEffect(this);
    }

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

    protected void CheckExpire()
    {
        if (lastPushTime + decayTime < Time.time)
        {
            RemoveEffect();
        }
    }

    protected void RemoveEffect()
    {
        target.GetComponent<EnemyBehaviour>().RemoveEffectByName(ToString());
    }
}