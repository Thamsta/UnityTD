using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlaceTower : MonoBehaviour {

    public Text _notifyText;
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
        if (canPlaceTower())
        {
            print(_selectTower.activeTower);
            if(_selectTower.activeTower == null)
            {
                _notifyText.GetComponent<Text>().text = "Select a tower first";
            }
            else
            {
            _tower = (GameObject) Instantiate(_selectTower.activeTower, transform.position, Quaternion.identity);
            _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
            }
        }
        
        else if(canUpgradeTower())
        {
            _tower.GetComponent<TowerData>().increaseLevel();
            _gameManager.Gold -= _tower.GetComponent<TowerData>().CurrentLevel.cost;
        }
    }

    private bool canPlaceTower()
    {
        int cost = _selectTower.activeTower.GetComponent<TowerData>()._levels[0].cost;
        return _tower == null && (_gameManager.Gold >= cost);
    }

    private bool canUpgradeTower()
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