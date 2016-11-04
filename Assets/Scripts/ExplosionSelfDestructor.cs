using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExplosionSelfDestructor : MonoBehaviour
{

    private float spawnTime;

    // Use this for initialization
    void Awake()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > 1)
        {
           Destroy(gameObject);
        }
    }
}
