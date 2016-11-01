using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start () {
		lastEnemyCounter = 0;
		lastWaveEndTime = 0;
		lastEnemySpawnTime = 0;

		enemiesToSpawn = enemySpawnCounter;
	}

	// Update is called once per frame
	void Update () {

		//Last enemy just died and wave has ended
		if (CountEnemies () == 0 && enemiesToSpawn == 0) {
			if (lastEnemyCounter != 0) {

				//When did the wave end?
				lastWaveEndTime = Time.time;

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
			}
		}

		//Count the enemies
		lastEnemyCounter = CountEnemies ();
	}

	public void Spawn() {


		lastEnemySpawnTime = Time.time;
		enemiesToSpawn = enemiesToSpawn - 1;

        //Spawns an enemy of a random type, maybe add a given order?
		int enemyType = Mathf.RoundToInt (Mathf.Clamp (0, Random.value * (enemyPrefabs.Length - 1), enemyPrefabs.Length - 1));

		Debug.Log ("Spawned Enemy of type: " + enemyType.ToString ());

		GameObject enemy = Instantiate (enemyPrefabs[enemyType]);
		enemy.GetComponent <EnemyMovement> ().waypoints = waypoints;
	}

	public int CountEnemies () {
		return GameObject.FindGameObjectsWithTag ("Enemy").GetLength (0);
	}
}

