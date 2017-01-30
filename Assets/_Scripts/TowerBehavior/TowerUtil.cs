using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ShowRange();
	}

    public void ShowRange()
    {
        float r = GetComponent<CapsuleCollider>().radius;
        Material newMat = Resources.Load("RangeIndicator", typeof(Material)) as Material;

        GameObject range = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        range.transform.localScale = new Vector3(2 * r, 0.000001f, 2 * r);
        range.GetComponent<Renderer>().material = newMat;
        range.layer = LayerMask.NameToLayer("Ignore Raycast");

        range.transform.SetParent(gameObject.transform, false);
    }
}
