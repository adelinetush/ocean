using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;

    [SerializeField] private int m_currentScore;

    private void Awake()
    {
        m_scoreText.text = "Score: " + m_currentScore;
    }
    void UpdateScore()
    {
        if (m_currentScore != ScoreManager.ScoreManagerInstance.CurrentScore)
        {
            m_currentScore = ScoreManager.ScoreManagerInstance.CurrentScore;
            m_scoreText.text = "Score: " + m_currentScore.ToString();
        }
    }
    //Subscribe to score changes
    private void OnEnable()
    {
        ScoreManager.ScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        ScoreManager.ScoreUpdated -= UpdateScore;
    }
}
