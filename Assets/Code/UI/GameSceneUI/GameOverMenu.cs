using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public RawImage rawImage;

    private void Start()
    {
        GameManager.Instance.currentGameStates = GameStates.inMenu;
        MusicManager.Instance.PlayMusic(MusicManager.Instance.menuSong);
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        SceneManagement.Instance.LoadScene(rawImage, SceneList.StartScreen.ToString());
    }

    public void Quit()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        Application.Quit();
    }

    private void OnDisable()
    {
        
    }
}
