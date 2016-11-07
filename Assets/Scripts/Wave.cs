using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wave : MonoBehaviour {

	public int enemySpawnCounter;
	public int enemiesToSpawn;
	public int specialEnemiesToSpawn;

	public int lastEnemyCounter;
	public float lastEnemySpawnTime;

	GameObject[] enemyPrefabs;
	GameObject[] waypoints;

	WaveInfo waveInfo;

	public Wave(WaveInfo waveInfo, GameObject[] o, GameObject[] w) {
		enemyPrefabs = o;
		waypoints = w;
		waveInfo = waveInfo;
	}

	public void SpawnNext() {
		lastEnemySpawnTime = Time.time;
		enemiesToSpawn = enemiesToSpawn - 1;

		//TODO: add determined wave behavior
		//Spawns an enemy of a random type, maybe add a given order?
		int enemyType = Mathf.RoundToInt (Mathf.Clamp (0, Random.value * (enemyPrefabs.Length - 1), enemyPrefabs.Length - 1));

		GameObject enemy = Instantiate (enemyPrefabs[enemyType]);
		enemy.GetComponent <EnemyMovement> ().waypoints = waypoints; 
	}

	/// <summary>
	/// Generates SpawnList for wave
	/// </summary>
	/// <returns>The spawn list.</returns>
	public SortedList GenerateSpawnList() {

		//TODO GENERATE ALGORITHM EXTREME

		//Logistisches Wachstum?
		//Begrenztes Wachstum?
		return null;
	}


	//Generates a list for special enemies which are to spawn immediately
	public SortedList GenerateSpecialSpawn () {
		return null;
	}
}

struct WaveInfo {

	public float enemySpawnInterval;
	public int enemySpawnCounter;

	//Spawn order of enemy types
	public SortedList spawnOrder;
	public List<KeyValuePair<int, int>> spawnData;

	public WaveInfo(List<KeyValuePair<int, int>> data, SortedList spawnOrder, float enemySpawnInterval, int enemySpawnCounter) {
		this.enemySpawnInterval = enemySpawnInterval;

		this.enemySpawnCounter = enemySpawnCounter;
		spawnData = data;
		this.spawnOrder = spawnOrder;
	}

	public int GetEnemySpawnCounter() {

		int enemySpawnCounter;

		foreach (KeyValuePair<int,int> pair in spawnData) {
			enemySpawnCounter += pair.Value;
		}

		return enemySpawnCounter;
	}

}