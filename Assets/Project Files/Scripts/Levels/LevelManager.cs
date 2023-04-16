using UnityEngine;
using static GameManager;
using static ScoreManager;

public class LevelManager : MonoBehaviour
{
    //Handles loading of next level when one level is complete 
    //Also sets the game state to running when a level is running
    private void Start()
    {
        GameManagerInstance.CurrentState = GameState.GAME;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        // Do something with the new game state
        Debug.Log(GameManagerInstance.CurrentState);
    }

    private void OnEnable()
    {
        GameManagerInstance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManagerInstance.OnGameStateChanged -= HandleGameStateChanged;
    }
}
