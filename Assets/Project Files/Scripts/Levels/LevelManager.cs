using UnityEngine;
using static GameManager;
using static ScoreManager;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        GameManagerInstance.CurrentState = GameState.GAME;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        // Do something with the new game state
        Debug.Log(GameManagerInstance.CurrentState);
    }

    private void HandleLevelComplete(bool winStatus)
    {
        GameManagerInstance.LoadNextLevel();
    }

    private void OnEnable()
    {
        GameManagerInstance.OnGameStateChanged += HandleGameStateChanged;
        ScoreManagerInstance.OnGameOver += HandleLevelComplete;
    }

    private void OnDisable()
    {
        GameManagerInstance.OnGameStateChanged -= HandleGameStateChanged;
        ScoreManagerInstance.OnGameOver -= HandleLevelComplete;
    }
}
