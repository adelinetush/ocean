using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public delegate void GameStateChangedHandler(GameState newState);

    public event GameStateChangedHandler OnGameStateChanged;

    public enum GameState { LOADING, PAUSE, GAME }

    private static GameManager _gameManagerInstance;
    public static GameManager GameManagerInstance
    {
        get
        {
            if (_gameManagerInstance == null)
                Debug.LogWarning("Game Manager is null");

            return _gameManagerInstance;
        }
    }

    private GameState _currentState;
    public GameState CurrentState
    {
        get { return _currentState; }
        set
        {
            if (_currentState != value)
            {
                _currentState = value;
                OnGameStateChanged?.Invoke(value);
            }
        }
    }

    private int _currentLevel;
    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    private int _nextLevel;
    public int NextLevel
    {
        get { return _nextLevel; }
        set { _nextLevel = value; }
    }

    [SerializeField] private List<string> m_playableLevels;

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.0f);
        LoadGame();
    }

    private void Awake()
    {
        _gameManagerInstance = this;
    }

    private void LoadGame()
    {
        SetLevels(0, 1);
        SceneManager.LoadSceneAsync(m_playableLevels[CurrentLevel], LoadSceneMode.Additive);
        CurrentState = GameState.LOADING;

        ScoreManager.OnNextLevelLoaded?.Invoke();
        Debug.Log(CurrentState);
    }

    private void SetLevels(int currentLevel, int nextLevel)
    {
        CurrentLevel = currentLevel;
        NextLevel = nextLevel;
    }

    public void LoadNextLevel()
    {
        if (NextLevel < m_playableLevels.Count)
        {
            SceneManager.LoadSceneAsync(m_playableLevels[NextLevel], LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(m_playableLevels[CurrentLevel]);
            CurrentLevel = NextLevel;
            NextLevel++;

            ScoreManager.OnNextLevelLoaded?.Invoke();
        } else
        {
            Debug.Log("Game Over");
        }
    }

}
