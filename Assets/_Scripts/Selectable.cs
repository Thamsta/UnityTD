using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    public Material selected;
    private Material original;

    void Start()
    {
        original = GetComponent<Renderer>().material;
    }

    public void select()
    {
        GetComponent<Renderer>().material = selected;
    }

    public void deselect()
    {
        GetComponent<Renderer>().material = original;
    }
}
