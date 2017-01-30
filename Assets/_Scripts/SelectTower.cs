using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectTower : MonoBehaviour
{
    private GameObject activeTower;
    public GameObject onTowerHUD;

    //selected Tower from the "shop"
    public GameObject ActiveTower
    {
        get { return activeTower; }
        set
        {
            activeTower = value;

            if(activeTower != null)
            {
                //Try to find the TowerHUD and set it inactive
                if (GameObject.Find("OnTowerHUD"))
                {
                    GameObject.Find("OnTowerHUD").GetComponent<HUDBehavior>().ActivePlatform = null;
                }
            }
            SetCursor();
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

    // Sets the Sprite for the cursor depending on the selected Mode
    void SetCursor()
    {
        if (activeTower != null)
        {
            Cursor.SetCursor(activeTower.GetComponent<TowerData>().cursorTexture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void ButtonClicked(GameObject tower)
    {
        ActiveTower = tower;
        GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().SetMessageLabelText("");
    }
}