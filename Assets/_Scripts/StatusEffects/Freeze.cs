using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : _StatusEffect {
    private float stunDuration;
    private float slow;
    private int limit;
    private int freezeCount = 1;
    private float lastPushTime;
    private float decayTime;

    public Freeze(float slow, int limit, float stunDuration, float decTime, GameObject target)
    {
        lastPushTime = Time.time;
        this.target = target;
        this.slow = slow;
        this.stunDuration = stunDuration;
        this.limit = limit;
        decayTime = decTime;
        name = "Freeze";
        target.GetComponent<EnemyBehaviour>().AddStatusEffect(this);
    }

	protected override bool CanApplyEffect()
    {
        if(lastPushTime + decayTime < Time.time)
        {
            RemoveEffect();
        }
        else if(freezeCount >= limit)
        {
            return true;
        }
        return false;
    }

    protected override void ApplyEffect()
    {
        //TODO: Apply Stuns
        RemoveEffect();
    }

    public override float MovementScale()
    {
        return System.Math.Max(0, 1 - (slow * 2 * freezeCount / limit));
    }

    public void incrementFreeze()
    {
        freezeCount++;
    }
}