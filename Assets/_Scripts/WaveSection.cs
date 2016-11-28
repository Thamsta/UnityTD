using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WaveSection : MonoBehaviour
{

    int currentEnemyIndex;
    int maxEnemyIndex;

    //Time of last enemy spawn
    float lastEnemySpawnTime;
    //Time between spawns
    float enemySpawnInterval;

    //A reference to all enemy prefabs.
    GameObject[] enemyPrefabs;

    //Wave info for this sections
    WaveInfo waveInfo;

    ArrayList enemySpawnList;

    /*
	 * First place sets spawnInterval
	 * Second place contains enemies.
	 */
    ArrayList specialEnemySpawnList;

    public WaveSection(WaveInfo waveInfo)
    {
        specialEnemySpawnList = new ArrayList();
    }

    public void SpawnNext()
    {
        if (CanEnemySpawn())
        {
            //Spawn enemy and assign waypoints
            //TODO MOVE TO SPAWN ENEMY
            currentEnemyIndex++;

            //Set spawn time of current enemy
            lastEnemySpawnTime = Time.time;
        }
    }

    /// <summary>
    /// Generates SpawnList for wave
    /// </summary>
    /// <returns>The spawn list.</returns>
    public ArrayList GenerateSpawnList()
    {
        ArrayList list = new ArrayList();

        for (int i = 0; i < waveInfo.GetSectionSize(); i++)
        {
            GameObject enemy = GetEnemy(i);

            list.Add(enemy);
        }

        //Logistisches Wachstum?
        //Begrenztes Wachstum?
        return list;
    }

    private GameObject GetEnemy(int currentEnemyIndex)
    {
        return null;
    }

    bool CanEnemySpawn()
    {
        return (enemySpawnList.Count != 0) && (Time.time - lastEnemySpawnTime > enemySpawnInterval);
    }

    public bool HasSectionFinished()
    {
        return specialEnemySpawnList.Count == 0 && enemySpawnList.Count == 0;
    }
}

