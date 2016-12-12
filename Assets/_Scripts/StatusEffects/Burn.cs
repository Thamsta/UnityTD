using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : _StatusEffect {

    private int limit;
    private int damage;
    private GameObject target;
    public Burn(int damage, int limit, float decTime, GameObject target) : base("Burn", target, decTime)
    {
        this.limit = limit;
        decayTime = decTime;
        this.damage = damage;
        this.target = target;
    }

    /// <summary>
    /// Checks if the effect can be applied. Gets called every frame by the enemy it is attached to.
    /// </summary>
    /// <returns>can effect be applied</returns>

    protected override void ApplyEffect()
    { 
        EnemyBehaviour targetBehaviour = target.GetComponent<EnemyBehaviour>();
        targetBehaviour.receiveDamage(damage);
    }

}
