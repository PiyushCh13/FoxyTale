using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public float SFX_SLIDER;
    public float MUSIC_SLIDER;
    public List<SaveLevelData> levels;

    public SaveData()
    {
        levels = new List<SaveLevelData>();
    }
    public SaveData(float sfx, float music , List<LevelDataSO> levelDatas)
    {
        SFX_SLIDER = sfx;
        MUSIC_SLIDER = music;
        levels = new List<SaveLevelData>();
        foreach (var levelData in levelDatas)
        {
            levels.Add(new SaveLevelData(levelData));
        }
    }
}

public class SaveLevelData
{
    public string levelName;
    public bool isUnlocked;
    public int diamondsCollected;
    public int diamondsTotal;

    public SaveLevelData()
    {
        levelName = "";
        isUnlocked = false;
        diamondsCollected = 0;
        diamondsTotal = 0;
    }


    public SaveLevelData(LevelDataSO levelData)
    {
        levelName = levelData.levelName.ToString();
        isUnlocked = levelData.isUnlocked;
        diamondsCollected = levelData.diamondsCollected;
        diamondsTotal = levelData.diamondsTotal;
    }
}
