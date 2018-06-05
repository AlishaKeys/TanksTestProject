using System;
using System.Collections.Generic; 
using System.Linq; 
using UniRx; 
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public enum GameState
    {
        Menu,
        Game,
        IP, 
        Game2Player
    }

    [System.Serializable]
    public class Panel
    {
        [HideInInspector]
        public string name;
        public GameState state;
        public GameObject[] gameObjects;
    }

    public GameState state;
    public Panel[] panels;

    [Header("Game Parameters")]
    int bestScore;
    int score;
    public int TotalScore
    {
        get
        {
            int total = score;
            return total;
        }
    }
    public int BestScore
    {
        get
        {
            int best = bestScore;
            return best;
        }
    }

    [Header("Buttons State")]
    private bool _initialized;

    [SerializeField] Button start1PlayerBttn;
    [SerializeField] Button start2PlayerBttn;

    [SerializeField] Button quitBttn;

    [SerializeField] Button pauseBttn;
    bool pauseOn;

    public static Action OnGenerationLevel, OnReset;

    private void Start()
    {
        SetGameState(GameState.Menu);
        Initialized();
    }

    private void Initialized()
    {
        if (_initialized)
        {
            return;
        }

        bestScore = PlayerPrefs.HasKey("bestScores") ? PlayerPrefs.GetInt("bestScores") : 0;

        start1PlayerBttn.onClick.AddListener(ButtonStart1Player);
        start2PlayerBttn.onClick.AddListener(ButtonStart2Player);
        quitBttn.onClick.AddListener(ButtonQuit);
        pauseBttn.onClick.AddListener(ButtonPause);

        _initialized = true;
    }

    private void OnValidate()
    {
        if (panels != null)
        {
            foreach (var panel in panels)
            {
                panel.name = panel.state.ToString();
            }
        }
    }

    public void SetGameState(string _state)
    {
        GameState gameState = (GameState)System.Enum.Parse(typeof(GameState), _state);
        SetGameState(gameState);
    }

    public void SetGameState(GameState _state)
    {
        state = _state;

        foreach (var panel in panels)
        {
            if (panel.state != state)
            {
                foreach (var go in panel.gameObjects)
                {
                    if (go != null)
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
        foreach (var panel in panels)
        {
            if (panel.state == state)
            {
                foreach (var go in panel.gameObjects)
                {
                    if (go != null)
                    {
                        go.SetActive(true);
                    }
                }
            }
        }
    }

    void ButtonPause()
    {
        if (pauseOn)
        {
            Time.timeScale = 0;
            pauseOn = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseOn = true;
        }
    }

    private void ButtonMainMenu()
    {
        SetGameState(GameState.Menu);
    }

    void ButtonStart1Player()
    {
        StartGame();
    }

    void ButtonStart2Player()
    {
        SetGameState(GameState.IP);
    }

    void StartGame()
    {
        OnGenerationLevel();
        SetGameState(GameState.Game);
        score = 0;
        UIController.Instance.UpdateScore();
        UIController.Instance.UpdateBestScores();

    }

    public void LoseGame()
    {
        SetGameState(GameState.Menu);
        OnReset();
    }

    void ButtonQuit()
    {
        Application.Quit();
    }

    public void AddScore(int add)
    {
        score += add;
        UIController.Instance.UpdateScore();
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScores", bestScore);
            UIController.Instance.UpdateBestScores();
        }

    }
}