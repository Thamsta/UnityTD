using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : _StatusEffect {
    private float freezeDuration;
    private float slow;
    private int limit;
    private int freezeCount = 1;

    public Freeze(float slow, int limit, float freezeDuration, float decTime, GameObject target) : base("Freeze", target, decTime)
    { 
        this.slow = slow;
        this.freezeDuration = freezeDuration;
        this.limit = limit;
        decayTime = decTime;
        
    }

    /// <summary>
    /// Checks if the effect can be applied. Gets called every frame by the enemy it is attached to.
    /// </summary>
    /// <returns>can effect be applied</returns>
	protected override bool CanApplyEffect()
    {
        CheckExpire();
        if(freezeCount >= limit)
        {
            return true;
        }
        return false;
    }

    protected override void ApplyEffect()
    {
        new FreezeStun(freezeDuration, target);
        RemoveEffect();
    }

    public override float MovementScale()
    {
        return System.Math.Max(0, 1 - (slow * 2 * freezeCount / limit));
    }

    public void IncrementFreeze()
    {
        lastPushTime = Time.time;
        freezeCount++;
    }
}