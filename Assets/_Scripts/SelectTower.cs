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
            ActiveTower = null;
        }
    }

    public GameObject ActiveTower
    {
        get { return activeTower; }
        set
        {
            activeTower = value;
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
        else if(sellMode)
        {
            //TODO: Add Cursor-sprite for Sell-Mode
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
        GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().SetMessageLabelText("Sell");
    }
}