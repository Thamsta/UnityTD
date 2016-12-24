using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDBehavior : MonoBehaviour {

    private GameObject activePlatform;
    public GameObject ActivePlatform
    {
        get { return activePlatform; }
        set
        {
            if(activePlatform != null)
            {
                activePlatform.GetComponent<Selectable>().deselect();
            }

            activePlatform = value;
            if (activePlatform == null)
            {
                gameObject.SetActive(false);
                
            }
            else
            {
                activePlatform.GetComponent<Selectable>().select();
                gameObject.SetActive(true);
                Move();
                //set the towerselector inactive
                GameObject.Find("TowerSelectPanel").GetComponent<SelectTower>().ActiveTower = null;
            }
        }
    }

    void OnAwake()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            ActivePlatform = null;
        }
    }

    private void Move()
    {
        gameObject.transform.position = activePlatform.transform.position;
    }

    public void SellTower()
    {
        activePlatform.GetComponent<PlaceTower>().SellTower();
        ActivePlatform = null;
    }

    public void UpgradeTower()
    {
        PlaceTower towerScript = activePlatform.GetComponent<PlaceTower>();
        towerScript.UpgradeTower();
        if(!towerScript.CanUpgradeTower())
        {
            ActivePlatform = null;
        }
    }
}