using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectTower : MonoBehaviour
{
    public GameObject activeTower;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void buttonClicked(GameObject tower)
    {
        activeTower = tower;
        GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().SetMessageLabelText("");
    }
}