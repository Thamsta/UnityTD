using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject[] enemyPrefabs;

	//Time between wave spawn
	public float waveSpawnInterval;
	public float enemySpawnInterval;

	public int enemySpawnCounter;
	public int enemiesToSpawn;
	public float lastEnemySpawnTime;

	public int lastEnemyCounter;
	public float lastWaveEndTime;

	Wave wave;

	public int waveCounter;

	private GameManagerBehavior gameManager;


	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehavior> ();

		lastEnemyCounter = 0;
		lastWaveEndTime = 0;
		lastEnemySpawnTime = 0;

		waveCounter = 0;

		enemiesToSpawn = enemySpawnCounter;
		gameManager.SetRemainingEnemies (enemySpawnCounter);
	}

	void Update () {
		//Last enemy just died and wave has ended
		if (CountEnemies () == 0 && enemiesToSpawn == 0) {
			if (lastEnemyCounter != 0) {

				//When did the wave end?
				lastWaveEndTime = Time.time;

				//sends information to GUIp
				gameManager.SetRemainingEnemies (enemySpawnCounter);
				Debug.Log ("WAVE CLEARED!");
			} 

			if (Time.time - lastWaveEndTime > waveSpawnInterval) {            

				enemiesToSpawn = enemySpawnCounter;
			}
		}

		//When the wave is ongoing an enemies have to be spawned
		if (enemiesToSpawn > 0 && Time.time - lastWaveEndTime > waveSpawnInterval) {
			//Spawn enemies in a set interval
			if (Time.time - lastEnemySpawnTime > enemySpawnInterval) {
				Spawn ();
				gameManager.DecrementRemainingEnemies ();
			}
		}

		//Count the enemies
		lastEnemyCounter = CountEnemies ();
	}

	public void Spawn () {


		lastEnemySpawnTime = Time.time;
		enemiesToSpawn = enemiesToSpawn - 1;

		//TODO: add determined wave behavior
		//Spawns an enemy of a random type, maybe add a given order?
		int enemyType = Mathf.RoundToInt (Mathf.Clamp (0, Random.value * (enemyPrefabs.Length - 1), enemyPrefabs.Length - 1));

		GameObject enemy = Instantiate (enemyPrefabs [enemyType], waypoints [0].transform.position, Quaternion.identity);
		enemy.GetComponent <EnemyBehaviour> ().waypoints = waypoints;
	}

	public int CountEnemies () {
		return GameObject.FindGameObjectsWithTag ("Enemy").GetLength (0);
	}
}