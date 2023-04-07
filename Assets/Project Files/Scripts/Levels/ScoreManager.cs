using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void GameOverHandler(bool gameOverWinStatus);

    public event GameOverHandler OnGameOver;


    private static ScoreManager _scoreManagerInstance;

    public static ScoreManager ScoreManagerInstance { get { return _scoreManagerInstance; } }
    [SerializeField] private int m_maxLevelWasteScore;

    [SerializeField] private int m_currentWasteScore;

    private LevelStats m_levelStats;
    public int CurrentWasteScore
    {
        get { return m_currentWasteScore; }
        set { 
            m_currentWasteScore = value;
            HandleWasteScoreUpdated();
        }
    }

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
        if (_scoreManagerInstance != null && _scoreManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _scoreManagerInstance = this;
        }
    }

    private void HandleWasteScoreUpdated ()
    {
        if (m_currentWasteScore > m_maxLevelWasteScore)
        {
            OnGameOver?.Invoke(true);
        }
    }

    private void TryGetLevelStats(out LevelStats levelStats)
    {
        levelStats = null;
        foreach (LevelStats levelData in ExtendedScriptableObject.GetAll<LevelStats>())
        {
            Debug.Log(levelData.name);
            if (levelData.currentLevel == GameManager.GameManagerInstance.CurrentLevel)
            {
                levelStats = levelData;
            }
        }

        SetLevelStats(levelStats);
    }

    private void SetLevelStats(LevelStats levelStats)
    {
        m_maxLevelWasteScore = levelStats.maxWasteAmount;
        Debug.Log(m_maxLevelWasteScore);
    }

    public void TriggerLevelStats()
    {
        TryGetLevelStats(out m_levelStats);
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
