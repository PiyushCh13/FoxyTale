using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

    public void PauseMenu() 
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        GameManager.Instance.currentGameStates = GameStates.isPaused;
        UIManager.Instance.OpenMenu(pausePanel);
        UIManager.Instance.AnimateMenu(pausePanel, Vector3.one);

       Time.timeScale = 0.0f;
    }
}
