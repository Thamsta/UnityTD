using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TowerLevel
{
    public int cost;
    public GameObject visualization;
    public GameObject _bullet;
    public float _fireRate;
}

public class TowerData : MonoBehaviour {

    public List<TowerLevel> _levels;
    public Texture2D cursorTexture;
    private TowerLevel _currentLevel;

    void OnEnable()
    {
        CurrentLevel = _levels[0];
    }

    public TowerLevel CurrentLevel
    {
        get
        {
            return _currentLevel;
        }

        set
        {
            _currentLevel = value;

            for(int i = 0; i < _levels.Count;i++)
            {
                if(_levels[i] == _currentLevel)
                {
                    _levels[i].visualization.SetActive(true);
                }
                else
                {
                    _levels[i].visualization.SetActive(false);
                }
            }
        }
    }

    public TowerLevel getNextLevel()
    {
        int currentLevelIndex = _levels.IndexOf(_currentLevel);
        int maxLevelIndex = _levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return _levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void increaseLevel()
    {
        int currentLevelIndex = _levels.IndexOf(_currentLevel);
        if (currentLevelIndex < _levels.Count - 1)
        {
            CurrentLevel = _levels[currentLevelIndex + 1];
        }
    }
}