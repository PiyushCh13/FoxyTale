using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelDataManager : Singleton<LevelDataManager>
{
    public LevelDataSO levelOne, levelTwo, levelThree, bossLevel;
    public LevelDataSO UpdateLevelData(LevelList levelList , LevelDataSO updatedSO)
    {
        switch(levelList)
        {
            case LevelList.LevelOne:
                levelOne.diamondsCollected = updatedSO.diamondsCollected;
                levelOne.isUnlocked = updatedSO.isUnlocked;
                return levelOne;
            case LevelList.LevelTwo:
                levelTwo.diamondsCollected = updatedSO.diamondsCollected;
                levelTwo.isUnlocked = updatedSO.isUnlocked;
                return levelTwo;
            case LevelList.LevelThree:
                levelThree.diamondsCollected = updatedSO.diamondsCollected;
                levelThree.isUnlocked = updatedSO.isUnlocked;
                return levelThree;
            case LevelList.BossLevel:
                bossLevel.diamondsCollected = updatedSO.diamondsCollected;
                bossLevel.isUnlocked = updatedSO.isUnlocked;
                return bossLevel;
            default:
                return null;
        }
    }

    public LevelDataSO GetLevelData(LevelList levelList)
    {
        switch(levelList)
        {
            case LevelList.LevelOne:
                return levelOne;
            case LevelList.LevelTwo:
                return levelTwo;
            case LevelList.LevelThree:
                return levelThree;
            case LevelList.BossLevel:
                return bossLevel;
            default:
                return null;
        }
    }

    public void ResetLevelData()
    {
        levelOne.diamondsCollected = 0;
        levelTwo.diamondsCollected = 0;
        levelThree.diamondsCollected = 0;
        bossLevel.diamondsCollected = 0;

        levelOne.isUnlocked = true;
        levelTwo.isUnlocked = false;
        levelThree.isUnlocked = false;
        bossLevel.isUnlocked = false;
    }

}
