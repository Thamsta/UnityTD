using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerBehavior : MonoBehaviour {

    public Text goldLabel;
    public Text healthLabel;

    private int gold;
    private int health;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "Gold: " + gold;
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthLabel.GetComponent<Text>().text = "Health: " + health;
            if(health <= 0)
            {
                //TODO: Game over;
            }
        }
    }

    void Start()
    {
        Gold = 100000;
        Health = 5;
    }
}
