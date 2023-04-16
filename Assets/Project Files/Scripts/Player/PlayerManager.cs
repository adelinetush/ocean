//Handles character instantiation depending on the level
//The main character isn't always the same in every level
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //the character options available 
    [SerializeField] private GameObject m_diverPlayer;
    [SerializeField] private GameObject m_boatPlayer;

    //type of character to instantiate is defined in level stats
    private LevelData m_levelData;

    //Enum for the player type
    [SerializeField] private Enum_PlayerType m_playerType;

    private void Awake()
    {
        //get level data
        TryGetLevelStats(out m_levelData);
    }

    private void TryGetLevelStats(out LevelData levelStats)
    {
        levelStats = null;

        //find the level data scriptable object that matches the level
        foreach (LevelData levelData in ExtendedScriptableObject.GetAll<LevelData>())
        {
            if (levelData.currentLevel == GameManager.GameManagerInstance.CurrentLevel)
            {
                levelStats = levelData;
            }
        }

        //assign the player to instantiate
        m_playerType = levelStats.playerType;

        //instantiate the player 
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        //determines which player prefab and instantiates it 
        //depends on the player type
        switch (m_playerType)
        {
            case Enum_PlayerType.CHARACTER:
                GameObject playerDiver = Instantiate(m_diverPlayer);
                playerDiver.transform.SetParent(transform, false);
                break;
            case Enum_PlayerType.BOAT:
                GameObject playerBoat = Instantiate(m_boatPlayer);
                playerBoat.transform.SetParent(transform, false);
                break;
            default:
                Debug.LogError("Invalid player type");
                break;
        }
    }
}
