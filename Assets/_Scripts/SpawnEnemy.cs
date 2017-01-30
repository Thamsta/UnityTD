using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {

	public GameObject[] waypoints;
	public GameObject[] enemyPrefabs;

	//Time between wave spawn
	public float enemySpawnInterval;

	public int enemySpawnCounter;
	private int enemiesToSpawn;
	private float lastEnemySpawnTime;

	private int lastEnemyCounter;

    private bool waveActive;

	Wave wave;

	private int waveCounter;

	private GameManagerBehavior gameManager;


	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior> ();

		lastEnemyCounter = 0;
		lastEnemySpawnTime = 0;

		waveCounter = 0;

        waveActive = false;

		enemiesToSpawn = 0;
		gameManager.SetRemainingEnemies (enemySpawnCounter);
	}

	void Update () {
		//Last enemy just died and wave has ended
		if (CountEnemies () == 0 && enemiesToSpawn == 0 && gameManager.State == GameManagerBehavior.GameState.Running) {
            //Moment when in the current frame are no enemies and in the frame before were
            if (lastEnemyCounter != 0) {

				//sends information to the gameManager
				gameManager.SetRemainingEnemies(enemySpawnCounter);
                gameManager.SetWaveButton();
                waveActive = false;
				Debug.Log ("WAVE CLEARED!");
			} 

            //Next Wave Implementation
			if (waveActive) {
				enemiesToSpawn = enemySpawnCounter;
                waveCounter++;
                gameManager.SetWaveLabel(waveCounter);
			}
		}

        //When the wave is ongoing an enemies have to be spawned
		if (enemiesToSpawn > 0 && waveActive) {
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

    public void NextWave()
    {
        waveActive = true;
    }
}