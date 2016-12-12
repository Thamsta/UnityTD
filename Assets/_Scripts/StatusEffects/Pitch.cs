using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pitch : _StatusEffect{

    private float slow;
    private int limit;
    public Pitch(float slow, int limit, float decTime, GameObject target) : base("Pitch", target, decTime)
    { 
        this.slow = slow;
        this.limit = limit;
    }

    /// <summary>
    /// Checks if the effect can be applied. Gets called every frame by the enemy it is attached to.
    /// </summary>
    /// <returns>can effect be applied</returns>
    public override float MovementScale()
    {
        return System.Math.Max(0, 1 - (slow / limit));
    }

    protected override void ApplyEffect()
    {
    }
}
