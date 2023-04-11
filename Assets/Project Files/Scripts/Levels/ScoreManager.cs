using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    //Game over event for other scripts to subscribe
    public delegate void GameOverHandler(bool gameOverWinStatus);

    public event GameOverHandler OnGameOver;


    //this is a sigleton 
    private static ScoreManager _scoreManagerInstance;

    public static ScoreManager ScoreManagerInstance { get { return _scoreManagerInstance; } }

    //Amount of waste to be collected
    [SerializeField] private int m_maxLevelWasteScore;

    //amount of waste that has been collected 
    [SerializeField] private int m_currentWasteScore;


    //level data variable
    private LevelData m_levelData;

    //amount of waste that's collected 
    //get and set
    public int CurrentWasteScore
    {
        get { return m_currentWasteScore; }
        set { 
            m_currentWasteScore = value;
            HandleWasteScoreUpdated();
        }
    }

    //whenever a new level is loaded, handles the search for that level data 
    public static Action OnNextLevelLoaded;

    public int MaxLevelWasteScore
    {
        set
        {
            m_maxLevelWasteScore = value;
        }
    }
    private void Awake()
    {
        //singleton handler for this class 
        if (_scoreManagerInstance != null && _scoreManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _scoreManagerInstance = this;
        }
    }

    //handles score and game over conditions 
    private void HandleWasteScoreUpdated ()
    {
        if (m_currentWasteScore > m_maxLevelWasteScore)
        {
            OnGameOver?.Invoke(true);
        }
    }

    //Searches scriptable objects of type LevelData to find the level data for the current level 
    private void TryGetLevelStats(out LevelData levelDataDetails)
    {
        levelDataDetails = null;
        foreach (LevelData levelData in ExtendedScriptableObject.GetAll<LevelData>())
        {
            if (levelData.currentLevel == GameManager.GameManagerInstance.CurrentLevel)
            {
                levelDataDetails = levelData;
            }
        }

        //sets the level data variables for that level
        SetLevelData(levelDataDetails);
    }

    private void SetLevelData(LevelData levelStats)
    {
        m_maxLevelWasteScore = levelStats.maxWasteAmount;
        Debug.Log(m_maxLevelWasteScore);
    }

    //to handle finding the right level data for a level when it is loaded 
    public void TriggerLevelStats()
    {
        TryGetLevelStats(out m_levelData);
    }

    private void Start()
    {
        OnNextLevelLoaded += TriggerLevelStats;
    }

    private void OnDestroy()
    {
        OnNextLevelLoaded -= TriggerLevelStats;
    }
}
