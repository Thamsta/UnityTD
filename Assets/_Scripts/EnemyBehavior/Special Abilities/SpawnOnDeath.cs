using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour {

    public int newEnemies;


    void OnDestroy()
    {
        if(gameObject.GetComponentInChildren<EnemyMovement>().reachedEnd)
        {
            Debug.Log("Spawn new Enemy!");

            for (int i = 0; i < newEnemies; i++)
            {
                //TODO: Spawn a new Enemy of the same type!
                //something like: spawn.SpawnEnemy(gameObject);
            }
        }
    }
}