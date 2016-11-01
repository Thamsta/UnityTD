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
        if (CanPlaceTower())
        {
            _tower = (GameObject) Instantiate(_selectTower.ActiveTower, transform.position, Quaternion.identity);
            _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
        }
        
        else if(CanUpgradeTower())
        {
            _tower.GetComponent<TowerData>().increaseLevel();
            _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
        }
    }

    private bool CanPlaceTower()
    {
        if (_selectTower.ActiveTower == null)
        {
            _gameManager.SetMessageLabelText("Select a tower first");
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
        }
        return false;
    }
}