using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerBehavior : MonoBehaviour {
    private readonly float messageShowTime = 1.3f;

    public Text goldLabel;
    public Text healthLabel;
    public Text messageLabel;
    public Text waveLabel;
    public Text remainLabel;

    private float lastMessageUpdate;

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
        Gold = 10000;
        Health = 5;
    }

    void Update()
    {
        if(Time.time - lastMessageUpdate > messageShowTime)
        {
            ClearMessageLabel();
        }
    }

    void ClearMessageLabel()
    {
        //TODO: fade out the text
        SetMessageLabelText("");
    }

    public void SetMessageLabelText(string newText)
    {
        messageLabel.GetComponent<Text>().text = newText;
        lastMessageUpdate = Time.time;
    }

    public void SetWaveLabel(int wave)
    {
        waveLabel.GetComponent<Text>().text = "Wave: " + wave;
    }

    public void SetRemainingEnemies(int remain)
    {
        remainLabel.GetComponent<Text>().text = "Remaining enemies: " + remain;
    }

    /// <summary>
    /// Reads the current value of remaining enemies and decrements it
    /// </summary>
    public void DecrementRemainingEnemies()
    {
        int n = 0;
        string text = remainLabel.GetComponent<Text>().text;
        for (int i = text.Length - 1; i > 0; i--)
        {
            char x = text[i];
            if (x != ' ')
            {
               n += (x - '0') * (int)System.Math.Pow(10, (text.Length - 1 - i));
                
            }
            else
            {
                break;
            }
        }
            SetRemainingEnemies(n - 1);
    }
}