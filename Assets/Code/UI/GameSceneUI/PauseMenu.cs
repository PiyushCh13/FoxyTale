using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public RawImage rawImage;

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.currentGameStates = GameStates.inMenu;
        SceneManagement.Instance.LoadScene(rawImage, SceneList.StartScreen.ToString());
        UIManager.Instance.AnimateMenu(this.gameObject, Vector3.zero);
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        Application.Quit();
    }

    public void Resume()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.currentGameStates = GameStates.isPlaying;
        Time.timeScale = 1.0f;
        UIManager.Instance.AnimateMenu(this.gameObject, Vector3.zero);
        this.gameObject.SetActive(false);
    }
}
