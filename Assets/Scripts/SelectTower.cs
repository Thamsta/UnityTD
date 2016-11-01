﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectTower : MonoBehaviour
{
    [HideInInspector]
    private GameObject activeTower;

    public GameObject ActiveTower
    {
        get { return activeTower; }
        set
        {
            activeTower = value;
            if(activeTower != null)
            {
                Cursor.SetCursor(activeTower.GetComponent<TowerData>().cursorTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            ActiveTower = null;
        }
    }

    public void ButtonClicked(GameObject tower)
    {
        ActiveTower = tower;
        GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().SetMessageLabelText(""); 
    }
}