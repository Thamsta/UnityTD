﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBulletBehavior : BulletBehavior {

    private List<GameObject> enemiesInRange = new List<GameObject>();

    //overrides automatically
    void Update () {
        Move();
        if (gameObject.transform.position.Equals(targetPosition))
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                DealDamage(enemy);
            }
            Destroy(gameObject);
        }
    }

    
    //Triggerfunctions
    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }
}