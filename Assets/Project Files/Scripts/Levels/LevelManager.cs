using UnityEngine;
using static GameManager;
using static ScoreManager;

public class LevelManager : MonoBehaviour
{
    //Handles loading of next level when one level is complete 
    //Also sets the game state to running when a level is running

    //level data variable
    private LevelData m_levelData;

    //will be displayed on game over screen
    public static string m_levelCompleteMessage;

    private void Start()
    {
        TryGetLevelStats(out m_levelData);
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
        m_levelCompleteMessage = levelStats.levelDescription;
    }
}
