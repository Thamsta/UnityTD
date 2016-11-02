using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectTower : MonoBehaviour
{
    private GameObject activeTower;
    private bool sellMode;

    public bool SellMode
    {
        get { return sellMode; }
        set
        {
            sellMode = value;
            activeTower = null;
            //TODO: Set cursor
        }
    }

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
        SellMode = false;
        ActiveTower = tower;
        GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().SetMessageLabelText(""); 
    }

    public void SellButtonClicked()
    {
        SellMode = true;
    }
}