using System.Collections;
using System.Collections.Generic;

public class Wave {

    //Here the types and quantity of enemies is stored

    private int currentEnemyIndex;
    private int[] enemySpawnList;

    public Wave()
    {
        currentEnemyIndex = 0;
    }

    /// <summary>
    /// Return the next enemy in the list and increments the index by one.
    /// </summary>
    /// <returns></returns>
    public int GetNextEnemyType()
    {
        currentEnemyIndex++;
        return enemySpawnList[currentEnemyIndex];
    }

    /// <summary>
    /// How many enemies are still going to be spawned
    /// </summary>
    /// <returns>Enemies still to be spawned</returns>
    public int CountEnemiesToSpawn()
    {
        return CountAllEnemies() - (currentEnemyIndex + 1);
    }

    /// <summary>
    /// How many enemies are contained inside the enemySpawnList
    /// </summary>
    /// <returns>Quantity of all enemies</returns>
    public int CountAllEnemies()
    {
        return enemySpawnList.Length;
    }
}
