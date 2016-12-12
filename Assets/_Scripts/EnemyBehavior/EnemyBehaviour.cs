using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour {

    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float originalSpeed;
    public float speed;
    public bool reachedEnd;
    private List<_StatusEffect> effects = new List<_StatusEffect>();

    // Use this for initialization
    void Start () {
        originalSpeed = speed;
        RotateIntoMoveDirection();
	}
	
	// Update is called once per frame
	void Update () {
        foreach (_StatusEffect e in effects.ToArray())
        { 
            e.TryApply();
        }
        ScaleSpeed();
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);


        if (gameObject.transform.position.Equals(endPosition))
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                RotateIntoMoveDirection();
            }
            else
            {
                reachedEnd = true;
                Destroy(gameObject);
                GameObject gm = GameObject.Find("GameManager");
                gm.GetComponent<GameManagerBehavior>().Health -= 1;
            }
        }
    }

    public void AddStatusEffect(_StatusEffect e)
    {
        effects.Add(e);
        print("Added " + e.ToString() + "!");
    }

    public void RemoveEffectByName(string s)
    {
        for(int i = 0; i < effects.Count; i++)
        {
            if (effects[i].ToString() == s)
            {
                effects.RemoveAt(i);
                ScaleSpeed();
                break;
            }
        }
    }

    public void receiveDamage(int damage)
    {
        Transform healthBarTransform = transform.FindChild("HealthBar");
        HealthBar healthBar =
            healthBarTransform.gameObject.GetComponent<HealthBar>();
        healthBar.currentHealth -= Mathf.Max(damage, 0);
        if(healthBar.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public _StatusEffect ContainsEffect(string s)
    {
        _StatusEffect status = null;
        foreach(_StatusEffect e in effects)
        {
            if(e.ToString() == s)
            {
                status = e;
            }
        }
        return status;
    }

    public void ScaleSpeed()
    {
        float scale = 1.0F;
        if (effects.Count > 0)
        {
            foreach (_StatusEffect e in effects)
            {
                scale *= e.MovementScale();
            }
        }
        speed = originalSpeed * scale;
    }
    /**
    /// <summary>
    /// Adds a movement impairment that expires after a time.
    /// </summary>
    /// <param name="scale">The scaling for the movespeed. 1.0 as the neutral element</param>
    /// <param name="duration">The time duration in seconds.</param>
    public void AddMovementImpairment(float scale, float duration)
    {

    }

    /// <summary>
    /// Adds a permanent movement impairment. Returns the index so it can be manually deleted.
    /// </summary>
    /// <param name="scale">The scaling for the movespeed. 1.0 as the neutral element</param>
    public int AddMovementImpairment(float scale)
    {
        permMoveImp.Add(scale);
        return permMoveImp.Count - 1;
    }

    public void removeMovementImpairment(int index)
    {
        permMoveImp[index] = 1.0F;
    }*/

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector3.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }

    private void RotateIntoMoveDirection()
    {
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        Vector3 newDirection = (newEndPosition - newStartPosition);

        float bufferX = newDirection.x;
        float bufferZ = newDirection.z;

        //TODO: Rotate HealthBar appropiately
        
        //print(mesh);
        transform.rotation = Quaternion.LookRotation(new Vector3(bufferZ,0,-bufferX), Vector3.up);

        Transform healthMesh = transform.Find("HealthBar");
        Quaternion bufferRot = Quaternion.LookRotation(-Camera.main.transform.forward);
        Quaternion newBufferRot = Quaternion.Euler(bufferRot.x, bufferRot.y, bufferRot.z);
        healthMesh.transform.rotation = newBufferRot;
    }
}