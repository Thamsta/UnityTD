using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeStun : _StatusEffect
{
    public FreezeStun(float rootDuration, GameObject target) : base("FreezeStun", target, rootDuration)
    {
    }

    protected override void ApplyEffect() { } 

                         
    public override float MovementScale()
    {
        return 0.0F;
    }
}
