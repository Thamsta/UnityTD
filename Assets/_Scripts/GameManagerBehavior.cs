using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {
    private readonly float messageShowTime = 1.3f;

    private Text goldLabel;
    private Text healthLabel;
    private Text messageLabel;
    private Text waveLabel;
    private Text remainLabel;
    private GameObject gameOverPanel;
    //private Animator anim;

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
                GameOver();
            }
        }
    }

    void Start()
    {
        InitialiseLabels();
        //anim = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        Health = 5;
        Gold = 10000;
    }

    private void InitialiseLabels()
    {
        goldLabel = (Text)GameObject.Find("GameCanvas/GoldLabel").GetComponent("Text");
        healthLabel = (Text)GameObject.Find("GameCanvas/HealthLabel").GetComponent("Text");
        waveLabel = (Text)GameObject.Find("GameCanvas/WaveLabel").GetComponent("Text");
        remainLabel = (Text)GameObject.Find("GameCanvas/RemainLabel").GetComponent("Text");
        messageLabel = (Text)GameObject.Find("GameCanvas/TowerSelectPanel/Warning").GetComponent("Text");
        gameOverPanel = GameObject.Find("GameCanvas/GameOverPanel");
    }

    void Update()
    {
        if(Time.time - lastMessageUpdate > messageShowTime)
        {
            ClearMessageLabel();
        }
    }

    void GameOver()
    {
        //anim.SetTrigger("GameOver");
        gameOverPanel.SetActive(true);
    }

    void ClearMessageLabel()
    {
        //TODO: fade out the text
        SetMessageLabelText("");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
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
        if(remainLabel == null)
        {
            remainLabel = (Text)GameObject.Find("GameCanvas/RemainLabel").GetComponent("Text");
        }
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