using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMovementOnLowHealth : MonoBehaviour {

    private HealthBar health;
    private EnemyBehaviour move;
    private int maxHealth;

    /// <summary>
    /// Relative movementspeed increase for each percent life lost.
    /// </summary>
    public float scale;

    void Start()
    {
        health = gameObject.GetComponentInChildren<HealthBar>();
        move = gameObject.GetComponentInChildren<EnemyBehaviour>();
    }

	// Update is called once per frame
	void Update ()
    {
        //relative Life:  0 - 100
        float relativeLife = health.RelativeLife();
        if (relativeLife != 100)
        {
            //move.ScaleSpeed(1 + ((100 - relativeLife) * (scale / 100)));
        }
	}
}
