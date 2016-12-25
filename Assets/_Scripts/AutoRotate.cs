using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    public float rotateSpeed;
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(new Vector3(0.0f, rotateSpeed, 0.0f));
	}
}
