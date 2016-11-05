using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

    public float speed = 10;
    public int damage;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Vector3 targetPosition;

    protected float distance;
    protected float startTime;

    protected GameManagerBehavior gameManager;

    void Start () {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
    }
	
	void Update () {
        Move();
        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                DealDamage();
            }
            Destroy(gameObject);
        }
    }

    protected void Move()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
    }

    protected void DealDamage()
    {
        Transform healthBarTransform = target.transform.FindChild("HealthBar");
        HealthBar healthBar =
            healthBarTransform.gameObject.GetComponent<HealthBar>();
        healthBar.currentHealth -= Mathf.Max(damage, 0);
        if (healthBar.currentHealth <= 0)
        {
            Destroy(target);
            //gameManager.Gold += 50;
        }
    }

    protected void DealDamage(GameObject otherTarget)
    {
        Transform healthBarTransform = otherTarget.transform.FindChild("HealthBar");
        HealthBar healthBar =
            healthBarTransform.gameObject.GetComponent<HealthBar>();
        healthBar.currentHealth -= Mathf.Max(damage, 0);
        if (healthBar.currentHealth <= 0)
        {
            Destroy(otherTarget);
            gameManager.Gold += 50;
        }
    }
}