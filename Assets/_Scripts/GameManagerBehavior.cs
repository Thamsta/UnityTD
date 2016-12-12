using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerBehavior : MonoBehaviour {
    private readonly float messageShowTime = 1.3f;

    private Text goldLabel;
    private Text healthLabel;
    private Text messageLabel;
    private Text waveLabel;
    private Text remainLabel;

    private float lastMessageUpdate;

    private int gold;
    private int health;

           
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            if (goldLabel != null)
            {
                goldLabel.GetComponent<Text>().text = "Gold: " + gold;
            }
            else
            {
                Debug.Log("GoldLabel not set");
            }
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
        InitialiseLabels();
        Health = 5;
        Gold = 10000;

    }

    private void InitialiseLabels()
    {
        goldLabel = (Text)GameObject.Find("Canvas/GoldLabel").GetComponent("Text");
        healthLabel = (Text)GameObject.Find("Canvas/HealthLabel").GetComponent("Text");
        waveLabel = (Text)GameObject.Find("Canvas/WaveLabel").GetComponent("Text");
        remainLabel = (Text)GameObject.Find("Canvas/RemainLabel").GetComponent("Text");
        messageLabel = (Text)GameObject.Find("Canvas/TowerSelectPanel/Warning").GetComponent("Text");
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