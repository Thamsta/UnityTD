using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TowerUtil{

    public static void ShowRange(GameObject tower, bool show)
    {
        if (show)
        {
            float r = tower.GetComponent<CapsuleCollider>().radius;
            Material newMat = Resources.Load("RangePreview", typeof(Material)) as Material;

            GameObject range = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            range.name = "range";
            range.transform.localScale = new Vector3(2 * r, 0.000001f, 2 * r);
            Vector3 locPos = range.transform.localPosition;
            locPos = tower.transform.InverseTransformPoint(0.0f, 0.1f, 0.0f);
            range.transform.localPosition = new Vector3(0.0f, locPos.y ,0.0f);
            range.GetComponent<Renderer>().material = newMat;
            range.layer = LayerMask.NameToLayer("Ignore Raycast");

            range.transform.SetParent(tower.transform, false);
        }
        else
        {
            GameObject range = tower.transform.Find("range").gameObject;
            Object.Destroy(range);
        }
    }
}
