using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    //Buttons
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_creditsButton;
    [SerializeField] private Button m_quitButton;
    [SerializeField] private Button m_nextLevelButton;
    [SerializeField] private Button m_pauseButton;

    //Pause Menu buttons 
    [SerializeField] private Button m_resumeButton;


    //Canvas groups
    [SerializeField] private CanvasGroup m_startMenu;
    [SerializeField] private CanvasGroup m_hudMenu;
    [SerializeField] private CanvasGroup m_levelCompleteMenu;
    [SerializeField] private CanvasGroup m_pauseMenu;

    private void Awake()
    {
        m_playButton.onClick.AddListener(LoadGame);
        m_resumeButton.onClick.AddListener(ResumeGame);
        m_pauseButton.onClick.AddListener(PauseGame);
        m_nextLevelButton.onClick.AddListener(delegate { LoadNextLevel(m_levelCompleteMenu); });
        Hide(m_hudMenu);
        Hide(m_levelCompleteMenu);
    }

    private void LoadGame()
    {
        GameManager.GameManagerInstance.LoadNextLevel();
        Hide(m_startMenu);
        Show(m_hudMenu);

        Time.timeScale = 1.0f;
    }

    private void PauseGame()
    {
        Show(m_pauseMenu);
        Time.timeScale = 0.0f;
    }

    private void ResumeGame()
    {
        Hide(m_pauseMenu);
        Time.timeScale = 1.0f;
    }

    private void LoadNextLevel(CanvasGroup levelCompleteMenu)
    {
        Hide(levelCompleteMenu);
        GameManager.GameManagerInstance.LoadNextLevel();
    }

    private void ShowLevelComplete()
    {
        GameManager.GameManagerInstance.UnloadPreviousLevel();
        Show(m_levelCompleteMenu);

        //Next level is disabled if there are no more levels
        m_nextLevelButton.GetComponent<Image>().color = GameManager.GameManagerInstance.NextLevel < GameManager.GameManagerInstance._totalLevels ? Color.white : Color.gray;
        m_nextLevelButton.enabled = GameManager.GameManagerInstance.NextLevel < GameManager.GameManagerInstance._totalLevels;

        //Reset score when level ends
        ScoreManager.ScoreManagerInstance.ResetScore();
    }

    public void ReturnToMainMenu()
    {
        GameManager.GameManagerInstance.UnloadPreviousLevel();
        ScoreManager.ScoreManagerInstance.ResetScore();

        Hide(m_levelCompleteMenu);
        Hide(m_hudMenu);
        Hide(m_pauseMenu);
        Show(m_startMenu);

        GameManager.GameManagerInstance.SetLevels(0);
    }

    protected void Show(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    protected void Hide(CanvasGroup canvasGroup) {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private void OnEnable()
    {
        ScoreManager.ScoreManagerInstance.OnGameOver += ShowLevelComplete;
    }

    private void OnDisable()
    {
        ScoreManager.ScoreManagerInstance.OnGameOver -= ShowLevelComplete;
    }
}
