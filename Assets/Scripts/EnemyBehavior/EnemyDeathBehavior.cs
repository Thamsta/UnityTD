using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathBehavior : MonoBehaviour {

    public int gold;

    private GameManagerBehavior gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

	void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.Gold += gold;
        }
    }
}