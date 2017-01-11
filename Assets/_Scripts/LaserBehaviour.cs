using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    LineRenderer lineRenderer;
    private GameObject target;
    public float yOffset;
    private Vector3 basePos;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        basePos = transform.position;
        basePos.y += yOffset;
        lineRenderer.SetPosition(0, basePos);
        lineRenderer.SetPosition(1, basePos);

	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
        {
            lineRenderer.SetPosition(1, target.transform.position);
        }
        else
        {
            lineRenderer.SetPosition(1, basePos);
        }
	}

    public void SetTarget(GameObject newTarget)
    {
        if(newTarget != null)
        {
            target = newTarget;
        }
        else
        {
            target = null;
        }
    }
}
