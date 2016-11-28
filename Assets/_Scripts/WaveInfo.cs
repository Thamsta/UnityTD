using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfo {

	public float enemySpawnInterval;
	public int waveSize;
	public int waveIndex;
	public int firstEnemyIndex, lastEnemyIndex;

	public WaveInfo (int waveIndex) {
		this.waveIndex = waveIndex;
	}

	public WaveInfo (int waveIndex, int firstEnemyIndex, int lastEnemyIndex) {
		this.waveIndex = waveIndex;
		this.firstEnemyIndex = firstEnemyIndex;
		this.lastEnemyIndex = lastEnemyIndex;
	}

	public int GetWaveSize () {
		return 0;
	}

	public ArrayList ReadConfig () {
		return null;
	}
}