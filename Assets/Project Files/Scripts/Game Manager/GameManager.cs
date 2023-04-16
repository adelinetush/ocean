using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public delegate void GameStateChangedHandler(GameState newState);

    public event GameStateChangedHandler OnGameStateChanged;

    public enum GameState { LOADING, MENU, GAME }

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
    public int _totalLevels;

    private void Awake()
    {
        _gameManagerInstance = this;
        _totalLevels = m_playableLevels.Count;
    }

    private void Start()
    {
        CurrentState = GameState.LOADING;
    }

    public void SetLevels(int nextLevel)
    {
        NextLevel = nextLevel;
    }

    public void LoadNextLevel()
    {
        //determines which level will be loaded next 
        //Game over if there are no more levels
        if (NextLevel < _totalLevels)
        {
            SceneManager.LoadSceneAsync(m_playableLevels[NextLevel], LoadSceneMode.Additive);
            CurrentLevel = NextLevel;
            NextLevel++;

            ScoreManager.OnNextLevelLoaded?.Invoke();
        } else
        {
            Debug.Log("Game Over");
        }
    }

    public void UnloadPreviousLevel() {

        Scene sceneToUnload = SceneManager.GetSceneByName(m_playableLevels[CurrentLevel]);
        if (sceneToUnload.isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }
    }

}
