using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_creditsButton;
    [SerializeField] private Button m_quitButton;

    [SerializeField] private CanvasGroup m_startMenu;
    [SerializeField] private CanvasGroup m_hudMenu;

    private void Awake()
    {
        m_playButton.onClick.AddListener(LoadGame);
        Hide(m_hudMenu);
    }

    private void LoadGame()
    {
        GameManager.GameManagerInstance.LoadNextLevel();
        Hide(m_startMenu);
        Show(m_hudMenu);
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
}
