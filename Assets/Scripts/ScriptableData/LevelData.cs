using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level /Data")]
public class LevelData : ScriptableObject
{
    public List<LevelDataList> LevelSet;
}

[System.Serializable]

public class LevelDataList
{
    public string LevelName;
    public GameMode gameMode;
    public bool isActive;
    public LevelDifficultyData m_difficulty;
    public string[] BlockSet;
    public float LevelTime;
}

public enum LevelDifficultyData
{
    Easy,
    Medium,
    Hard
}

