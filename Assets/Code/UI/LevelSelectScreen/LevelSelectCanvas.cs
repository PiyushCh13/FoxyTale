using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCanvas : MonoBehaviour
{
    [SerializeField] UIInputHandler inputManager;

    public RawImage image;

    public TMP_Text name_Text;
    public TMP_Text status_Text;
    public TMP_Text total_Diamonds;
    public TMP_Text collected_Diamond;

    public LevelDataSO levelOnedata;
    public LevelDataSO levelTwodata;
    public LevelDataSO levelThreedata;
    public LevelDataSO levelBossdata;

    public LevelDataSO currentLevelData;

    public GameObject panel;
    public MapPlayer player;

    private void Update()
    {
        if (player.currentWaypoint.isLevel) 
        {
            panel.SetActive(true);
        }

        else 
        {
            panel.SetActive(false);
        }

        switch (player.currentWaypoint.levelName) 
        {
            case SceneList.LevelOne:
                SetData(levelOnedata);
                break;
            case SceneList.LevelTwo:
                SetData(levelTwodata);
                break;
            case SceneList.LevelThree:
                SetData(levelThreedata);
                break;
            case SceneList.BossLevel:
                SetData(levelBossdata);
                break;

        }
    }

    private void Start()
    {
        inputManager.OnEnterLevel += OpenLevel;
    }


    public void OnClickBackButton() 
    {
        SceneManagement.Instance.LoadScene(image,SceneList.StartScreen.ToString());
    }

    public void SetData(LevelDataSO levelData) 
    {
        currentLevelData = levelData;
        name_Text.text = " Name:" + levelData.levelName.ToString();
        status_Text.text = " Unlocked:" + levelData.isUnlocked.ToString();
        total_Diamonds.text = " Diamonds:" + levelData.diamondsTotal.ToString();
        collected_Diamond.text = " Collected:" + levelData.diamondsCollected.ToString();
    }

    public void OpenLevel()
    {
        if (player.currentWaypoint.isLevel && currentLevelData.isUnlocked)
        {
            SFXManager.Instance.PlaySound(SFXManager.Instance.levelSelected);
            SceneManagement.Instance.LoadScene(image, player.currentWaypoint.levelName.ToString());
        }
    }
}
