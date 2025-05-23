using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameStates
{
    isPlaying,
    isPaused,
    GameOver,
    inMenu
}

public enum LevelList
{
    LevelOne,
    LevelTwo,
    LevelThree,
    BossLevel,
    None
}

public class GameManager : Singleton<GameManager>
{
    public GameStates currentGameStates;
    public List<string> unlockedLevels;
    public bool isMobile;

    void Start()
    {
        currentGameStates = GameStates.inMenu;
        unlockedLevels = new List<string>();
        AddUnlockLevel(LevelList.LevelOne);

        if (IsMobile())
        {
            isMobile = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
        }
        else
        {
            isMobile = false;
        }
    }

    public void AddUnlockLevel(LevelList levelList)
    {
        unlockedLevels.Add(levelList.ToString());
    }

    public void SaveLevelData(LevelList levelList)
    {
        switch (levelList)
        {
            case LevelList.LevelOne:
                PlayerPrefs.SetInt("LevelOneDiamonds", LevelDataManager.Instance.levelOne.diamondsCollected);
                PlayerPrefs.SetInt("LevelTwoUnlocked", LevelDataManager.Instance.levelTwo.isUnlocked ? 1 : 0);
                break;
            case LevelList.LevelTwo:
                PlayerPrefs.SetInt("LevelTwoDiamonds", LevelDataManager.Instance.levelTwo.diamondsCollected);
                PlayerPrefs.SetInt("LevelThreeUnlocked", LevelDataManager.Instance.levelThree.isUnlocked ? 1 : 0);
                break;
            case LevelList.LevelThree:
                PlayerPrefs.SetInt("LevelThreeDiamonds", LevelDataManager.Instance.levelOne.diamondsCollected);
                PlayerPrefs.SetInt("BossLevelUnlocked", LevelDataManager.Instance.bossLevel.isUnlocked ? 1 : 0);
                break;
        }

        PlayerPrefs.Save();
    }

    public void LoadLevelData()
    {
        if (PlayerPrefs.HasKey("LevelOneDiamonds"))
        {
            LevelDataManager.Instance.levelOne.diamondsCollected = PlayerPrefs.GetInt("LevelOneDiamonds");
        }

        if (PlayerPrefs.HasKey("LevelTwoDiamonds"))
        {
            LevelDataManager.Instance.levelTwo.diamondsCollected = PlayerPrefs.GetInt("LevelTwoDiamonds");
        }

        if (PlayerPrefs.HasKey("LevelThreeDiamonds"))
        {
            LevelDataManager.Instance.levelThree.diamondsCollected = PlayerPrefs.GetInt("LevelThreeDiamonds");
        }

        if (PlayerPrefs.HasKey("BossLevelUnlocked"))
        {
            LevelDataManager.Instance.bossLevel.isUnlocked = PlayerPrefs.GetInt("BossLevelUnlocked") == 1;
        }

        if (PlayerPrefs.HasKey("LevelTwoUnlocked"))
        {
            LevelDataManager.Instance.levelTwo.isUnlocked = PlayerPrefs.GetInt("LevelTwoUnlocked") == 1;
        }

        if (PlayerPrefs.HasKey("LevelThreeUnlocked"))
        {
            LevelDataManager.Instance.levelThree.isUnlocked = PlayerPrefs.GetInt("LevelThreeUnlocked") == 1;
        }
    }

    #region MobileCheck
    bool IsMobile()
    {
#if UNITY_ANDROID || UNITY_IOS
        return true;
#elif UNITY_WEBGL
        return IsMobileWebGL();
#else
        return false;
#endif
    }

    bool IsMobileWebGL()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    return DetectMobileDevice();
#else
        return false;
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool DetectMobileDevice();
#endif

    #endregion

}
