using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceTower : MonoBehaviour {


    private SelectTower _selectTower;
    private GameObject _tower;
    private GameManagerBehavior _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        _selectTower = GameObject.Find("TowerSelectPanel").GetComponent<SelectTower>();
    }

    void OnMouseUp()
    {
        if (_selectTower.SellMode && _tower != null)
        {
            _gameManager.Gold += CalculateRefund();
            Destroy(_tower);
            //unnecessary?
            print(_tower);
            _tower = null;
        }
        else if (CanPlaceTower())
        {
            _tower = (GameObject)Instantiate(_selectTower.ActiveTower, transform.position, Quaternion.identity);
            _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
        }
        else if (CanUpgradeTower())
        {
            _tower.GetComponent<TowerData>().increaseLevel();
            _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
        }
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

    private bool CanUpgradeTower()
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
            else
            {
                _gameManager.SetMessageLabelText("Tower is already at max level");
            }
        }
        return false;
    }
}