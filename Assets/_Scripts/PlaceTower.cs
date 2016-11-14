using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlaceTower : MonoBehaviour {


    private SelectTower _selectTower;
    private GameObject _tower;
    private GameManagerBehavior _gameManager;
    private HUDBehavior towerHUD;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        _selectTower = GameObject.Find("TowerSelectPanel").GetComponent<SelectTower>();
        towerHUD = GameObject.Find("OnTowerHUD").GetComponent<HUDBehavior>();
    }

    void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(_tower == null)
            {
                if (CanPlaceTower())
                {
                    _tower = (GameObject)Instantiate(_selectTower.ActiveTower, transform.position, Quaternion.identity);
                    _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
                }
            }
            else
            {
                towerHUD.ActivePlatform = gameObject;
            }
        }
    }

    public void UpgradeTower()
    {
        if (CanUpgradeTower())
        {
            _tower.GetComponent<TowerData>().increaseLevel();
            _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
        }
        else
        {
            _gameManager.SetMessageLabelText("Tower is already at max level");
        }
    }

    public void SellTower()
    {
        _gameManager.Gold += CalculateRefund();
        Destroy(_tower);
        //setting null is necessary
        _tower = null;
    }

    //Dynamically calculates the refund of a sold tower
    private int CalculateRefund()
    {
        TowerData activeTowerData = _tower.GetComponent<TowerData>();
        int refundGold = 0;
        int n = 0;

        while (activeTowerData._levels[n] != activeTowerData.CurrentLevel)
        { 
            refundGold += (activeTowerData._levels[n].cost);
            n++;
        }

        refundGold += (activeTowerData._levels[n].cost);
        //typecast to int rounds down
        refundGold = (int) (refundGold * 0.5);

        return refundGold;
    }

    private bool CanPlaceTower()
    {
        if (_selectTower.ActiveTower == null)
        {
            if (_tower == null) { _gameManager.SetMessageLabelText("Select a tower first"); }
            return false;
        }
        else
        {
            int cost = _selectTower.ActiveTower.GetComponent<TowerData>()._levels[0].cost;
            return _tower == null && (_gameManager.Gold >= cost);
        }
    }

    public bool CanUpgradeTower()
    {
        if (_tower != null)
        {
            TowerData towerData = _tower.GetComponent<TowerData>();
            TowerLevel nextLevel = towerData.getNextLevel();
            if(nextLevel != null)
            {
                int cost = nextLevel.cost;
                return _gameManager.Gold >= cost;
            }
        }
        return false;
    }
}