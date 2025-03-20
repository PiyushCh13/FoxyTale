using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour 
{
    [SerializeField] GameObject setting_Object;
    [SerializeField] GameObject control_Object;
    [SerializeField] GameObject mainmenu_Object;
    [SerializeField] RawImage image;

    private void Start()
    {
        GameManager.Instance.currentGameStates = GameStates.inMenu;
        MusicManager.Instance.PlayMusic(MusicManager.Instance.menuSong);
    }

    public void StartGame() 
    {
        GameManager.Instance.LoadLevelData();
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        SceneManagement.Instance.LoadScene(image, SceneList.LevelSelection.ToString());
    }

    public void NewGame()
    {
        LevelDataManager.Instance.ResetLevelData();
        GameManager.Instance.unlockedLevels.Clear();
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        SceneManagement.Instance.LoadScene(image, SceneList.LevelOne.ToString());
        PlayerPrefs.DeleteAll();
    }

    public void Settings() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(setting_Object, control_Object,mainmenu_Object);
        UIManager.Instance.AnimateMenu(setting_Object, Vector3.one);
    }

    public void Controls()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(control_Object, setting_Object, mainmenu_Object);
        UIManager.Instance.AnimateMenu(control_Object, Vector3.one);
    }

    public void Quit()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        Application.Quit();
    }
}
