using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelDataManager : Singleton<LevelDataManager>
{
    public LevelDataSO levelOne, levelTwo, levelThree, bossLevel;
    public LevelDataSO GetLevelData(LevelList levelList) => levelList switch
    {
        LevelList.LevelOne => levelOne,
        LevelList.LevelTwo => levelTwo,
        LevelList.LevelThree => levelThree,
        LevelList.BossLevel => bossLevel
    };
}
