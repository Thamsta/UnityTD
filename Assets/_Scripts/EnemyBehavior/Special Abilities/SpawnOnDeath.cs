using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour {

    public int newEnemies;


    void OnDestroy()
    {
        if(gameObject.GetComponentInChildren<EnemyBehaviour>().reachedEnd)
        {
            for (int i = 0; i < newEnemies; i++)
            {
                Debug.Log("Spawn new Enemy!");
                //TODO: Spawn a new Enemy of the same type!
                //something like: spawn.SpawnEnemy(gameObject);
            }
        }
    }
}