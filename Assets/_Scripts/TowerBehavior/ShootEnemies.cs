using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour {
    private List<GameObject> enemiesInRange;
    private float lastShotTime;
    private TowerData towerData;


    void Start () {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        towerData = gameObject.GetComponentInParent<TowerData>();
    }


    void Update()
    {
        GameObject target = null;

        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<EnemyMovement>().distanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > towerData.CurrentLevel._fireRate)
            {
                Shoot(target.GetComponent<Collider>());
                lastShotTime = Time.time;
            }
        }
    }

    void Shoot(Collider target)
    {
            GameObject bulletPrefab = towerData.CurrentLevel._bullet;

            Vector3 startPosition = gameObject.transform.position;
            Vector3 targetPosition = target.transform.position;
            startPosition.y = bulletPrefab.transform.position.y + 10;
            targetPosition.y = bulletPrefab.transform.position.y;
            GameObject newBullet = Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = targetPosition;
    }

    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del =
                other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }
}
