using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level_", menuName = "LevelStats")]
public class LevelData : ExtendedScriptableObject
{
    public int currentLevel;
    public int maxWasteAmount;
    public int maxTrapsDestroyed;
    public Enum_PlayerType playerType;
    public string levelDescription;
}
