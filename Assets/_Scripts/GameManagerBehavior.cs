using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {
    private readonly float messageShowTime = 1.3f;

    //HUD-Elements
    private Text goldLabel;
    private Text healthLabel;
    private Text messageLabel;
    private Text waveLabel;
    private Text remainLabel;

    private float lastMessageUpdate;

    //Menus 
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    
    //Some game state stuff
    public enum GameState { Running, Paused, GameOver};
    private GameState state;
    public GameState State
    {
        get { return state; }
        set
        {
            switch(value)
            {
                case GameState.Running:
                    {
                        state = value;
                        Run();
                        break;
                    }
                case GameState.Paused:
                    {
                        if(state != GameState.GameOver)
                        {
                            state = value;
                            Pause();
                        }
                        else { Debug.Log("can't go from gameover to pause"); }                       
                        break;
                    }
                case GameState.GameOver:
                    {
                        state = value;
                        GameOver();
                        break;
                    }
            }
        }
    }
    private bool fast;

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
                State = GameState.GameOver;
            }
        }
    }

    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            if (wave == 0)
            {
                waveLabel.GetComponent<Text>().text = "something texty";
            }
            else
            {
                waveLabel.GetComponent<Text>().text = "Wave: " + wave;
            }
        }
    }

    void Start()
    {
        InitialiseLabels();
        //anim = GetComponent<Animator>();
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        Health = 5;
        Gold = 10000;
        Wave = 1;

        fast = false;

        //TODO: reset mouse
        State = GameState.Running;
    }

    private void InitialiseLabels()
    {
        goldLabel = (Text)GameObject.Find("GameCanvas/GoldLabel").GetComponent("Text");
        healthLabel = (Text)GameObject.Find("GameCanvas/HealthLabel").GetComponent("Text");
        waveLabel = (Text)GameObject.Find("GameCanvas/WaveLabel").GetComponent("Text");
        remainLabel = (Text)GameObject.Find("GameCanvas/RemainLabel").GetComponent("Text");
        messageLabel = (Text)GameObject.Find("GameCanvas/TowerSelectPanel/Warning").GetComponent("Text");
        //gameOverPanel = GameObject.Find("GameCanvas/GameOverPanel"); public atm
        //pausePanel = GameObject.Find("GameCanvas/PausePanel");
    }

    void Update()
    {
        if(Time.time - lastMessageUpdate > messageShowTime)
        {
            ClearMessageLabel();
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(State == GameState.GameOver)
            {
                LoadMainMenu();
            }
            else
            {
            TogglePauseState();
            }
        }
    }

    void Run()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// Implementation for the Game Over state
    /// </summary>
    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    void TogglePauseState()
    {
        if(State == GameState.Paused)
        {
            State = GameState.Running;
        }
        else
        {
            State = GameState.Paused;
        }
    }

    /// <summary>
    /// Implementation for the Pause state
    /// </summary>
    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        State = GameState.Running;
    }

    public void ToggleFastMode(Button button)
    {
        if (fast)
        {
            fast = false;
            button.image.color = Color.white;
            Time.timeScale = 1.0f;
        }
        else
        {
            fast = true;
            button.image.color = new Color(0.7f,0.1f,0.1f);
            Time.timeScale = 3.0f;
        }
    }

    void ClearMessageLabel()
    {
        //TODO: fade out the text
        SetMessageLabelText("");
    }

    public void SetMessageLabelText(string newText)
    {
        if(messageLabel != null)
        {
        messageLabel.GetComponent<Text>().text = newText;
        lastMessageUpdate = Time.time;
        }else
        {
            print("messageLabel not Set");
        }
        
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

    public void SetWaveButton()
    {

    }
}