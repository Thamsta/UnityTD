using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    [HideInInspector]
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float originalSpeed;
    public float speed;

    // Use this for initialization
    void Start () {
        originalSpeed = speed;
        RotateIntoMoveDirection();
	}
	
	// Update is called once per frame
	void Update () {
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
                Destroy(gameObject);

                GameObject gm = GameObject.Find("GameManager");
                gm.GetComponent<GameManagerBehavior>().Health -= 1;
            }
        }
    }

    public void ScaleSpeed(float scale)
    {
        speed = originalSpeed * scale;
    }

    public float distanceToGoal()
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
        //Transform mesh = transform.Find("HealthBar");
        //print(mesh);
        transform.rotation = Quaternion.LookRotation(new Vector3(bufferZ,0,-bufferX), Vector3.up);
        
    }
}