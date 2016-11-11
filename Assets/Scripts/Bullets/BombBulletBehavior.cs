using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBulletBehavior : BulletBehavior {

    public GameObject explosion;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    protected override void OnDestinationReached()
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            DealDamage(enemy);
        }
        Instantiate(explosion).transform.position = gameObject.transform.position;
        Destroy(gameObject);
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