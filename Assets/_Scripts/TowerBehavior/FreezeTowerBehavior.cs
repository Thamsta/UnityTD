using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTowerBehavior : ShootEnemies {

    protected override void Shoot(Collider target)
    {
        if(target.gameObject.GetComponent<EnemyBehaviour>().ContainsEffect("Freeze") == null)
        {
            if (target.gameObject.GetComponent<EnemyBehaviour>().ContainsEffect("FreezeStun") == null)
            {
                new Freeze(0.3F, 10, 2.0F, 3.5F, target.gameObject);
            }
        }
        else
        {
            Freeze f = (Freeze)target.gameObject.GetComponent<EnemyBehaviour>().ContainsEffect("Freeze");
            f.IncrementFreeze();
        }
    }
}