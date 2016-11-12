using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The basic class that determines the Bullet Behavior. 
/// Classes that inherit from this class can change the behavior by tweaking the <block>Move</block>, <block>OnDestinationReached</block> or <block>DealDamage</block> functions.
/// </summary>
public abstract class BulletBehavior : MonoBehaviour {
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

    void Start()
    {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();
    }

	protected void Update () {
        Move();
        if (gameObject.transform.position.Equals(targetPosition))
        {
            OnDestinationReached();
        }
    }

    /// <summary>
    /// Moves the bullet towards the target position
    /// </summary>
    protected virtual void Move()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
    }

    /// <summary>
    /// Defines the behaviour if the bullet reached it's target-location
    /// </summary>
    protected virtual void OnDestinationReached()
    {
        if (target != null && damage != 0)
        {
            DealDamage();
        }
        ApplyEffect();
        Destroy(gameObject);
    }


    /// <summary>
    /// Deals Damage to a GameObject with a HealthBar script. Can't deal less then 0 Damage.
    /// If the health of the target drops below 0, the function destroys the GameObject.
    /// </summary>
    /// <param name="otherTarget">The target which should recieve damage</param>
    protected virtual void DealDamage(GameObject otherTarget)
    {
        Transform healthBarTransform = otherTarget.transform.FindChild("HealthBar");
        HealthBar healthBar =
            healthBarTransform.gameObject.GetComponent<HealthBar>();
        healthBar.currentHealth -= Mathf.Max(damage, 0);
        if (healthBar.currentHealth <= 0)
        {
            Destroy(otherTarget);
        }
    }

    /// <summary>
    /// Calls the DealDamage(gameObject) function with the default target
    /// </summary>
    protected virtual void DealDamage()
    {
        DealDamage(target);
    }

    protected void ApplyEffect() { }
}