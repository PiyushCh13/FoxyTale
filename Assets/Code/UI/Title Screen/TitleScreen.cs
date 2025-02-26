using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] Image blinkingImage;

    [SerializeField] UIInputHandler inputManager;

    [SerializeField] RawImage image;

    private void Start()
    {
        GameManager.Instance.currentGameStates = GameStates.inMenu;
        MusicManager.Instance.PlayMusic(MusicManager.Instance.menuSong);
        inputManager.OnPress += OnPressed;
        blinkingImage.DOFade(0f, 0.5f).SetLoops(-1,LoopType.Yoyo);
    }

    public void OnPressed()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        SceneManagement.Instance.LoadScene(image , SceneList.StartScreen.ToString());
    }

    private void OnDisable()
    {
        inputManager.OnPress -= OnPressed;
    }
}
