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

    protected virtual bool CanApplyEffect()
    {

        if (CheckExpire())
        {
            return false;
        }
        return true;

    }

    public virtual float MovementScale()
    {
        return 1.0F;
    }

    public override string ToString()
    {
        return name;
    }

    protected bool CheckExpire()
    {
        if (lastPushTime + decayTime < Time.time)
        {
            RemoveEffect();
            return true;
        }
        return false;
    }

    protected void RemoveEffect()
    {
        target.GetComponent<EnemyBehaviour>().RemoveEffectByName(ToString());
    }
}