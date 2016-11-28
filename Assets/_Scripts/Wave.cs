using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wave : MonoBehaviour {
	/*

	int currentEnemyIndex;
	int maxEnemyIndex;

	float lastEnemySpawnTime;
	float lastSpecialEnemySpawnTime;

	float specialEnemySpawnInterval;
	float enemySpawnInterval;

	bool isSpecialEnemySpawnEnabled;

	GameObject[] enemyPrefabs;
	GameObject[] waypoints;

	WaveInfo waveInfo;

	ArrayList enemySpawnList;

	/*
	 * First place sets spawnInterval
	 * Second place contains enemies.
	 * /
	ArrayList specialEnemySpawnList;

	public Wave () {
	}

	public void SpawnNext () {
		
		if (CanSpecialEnemySpawn ()) {
			StartCoroutine (SpecialEnemySpawn ());
		}

		if (CanEnemySpawn ()) {			

			//Spawn enemy and assign waypoints
			GameObject enemy = Instantiate (enemySpawnList [currentEnemyIndex], waypoints [0].transform.position, Quaternion.identity);
			enemy.GetComponent <EnemyMovement> ().waypoints = waypoints;

			currentEnemyIndex++;

			//Set spawn time of current enemy
			lastEnemySpawnTime = Time.time;
		}
	}

	IEnumerator SpecialEnemySpawn () {

		//Current enemy spawn index
		int currentSpecialEnemyIndex = 0;

		for (int i = 0; i < specialEnemySpawnList; i++) {

			//Spawn Enemy
			GameObject specialEnemy = Instantiate (specialEnemySpawnList [currentSpecialEnemyIndex], waypoints [0].transform.position, Quaternion.identity);
			specialEnemy.GetComponent <EnemyMovement> ().waypoints = waypoints;

			//Increse spawn index
			currentSpecialEnemyIndex++;

			lastSpecialEnemySpawnTime = Time.time;

			//Pause coroutine and wait for specified amount
			yield return new WaitForSeconds (specialEnemySpawnInterval);
		}

		isSpecialEnemySpawnEnabled = false;
	}

	/// <summary>
	/// Generates SpawnList for wave
	/// </summary>
	/// <returns>The spawn list.</returns>
	public SortedList GenerateSpawnList () {
		ArrayList list = new ArrayList ();

		for (int i = 0; i < waveInfo.GetWaveSize(); i++) {
			GameObject enemy = enemyPrefabs [0]; 

			list.Add (enemy);
		}

		//Logistisches Wachstum?
		//Begrenztes Wachstum?
		return list;
	}

	private GameObject GetEnemy() {
		
	}


	//Generates a list for special enemies which are to spawn immediately
	SortedList GenerateSpecialSpawn (ArrayList enemies) {		
		ArrayList list = enemies;

		return list;
	}

	bool CanEnemySpawn () {
		return (enemySpawnList.Count != 0) && (Time.time - lastEnemySpawnTime > enemySpawnInterval);
	}

	bool CanSpecialEnemySpawn () {
		return (specialEnemySpawnList.Count != 0) && (Time.time - lastSpecialEnemySpawnTime > specialEnemySpawnInterval) && !isSpecialEnemySpawnEnabled;
	}

	public bool HasWaveFinished () {
		return specialEnemySpawnList.Count == 0 && enemySpawnList.Count == 0;
	}*/
}

