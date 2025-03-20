using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public GameObject mainMenu;

    private void Start()
    {
        musicSlider.value = MusicManager.Instance.musicSource.volume;
        sfxSlider.value = SFXManager.Instance.sfxAudioSource.volume;
    }

    public void BackButton()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(mainMenu, this.gameObject);
        UIManager.Instance.AnimateMenu(this.gameObject, Vector3.zero);
        this.gameObject.SetActive(false);
    }

    public void MusicSlider()
    {
        MusicManager.Instance.musicSource.volume = musicSlider.value;
    }

    public void SFXSlider()
    {
        SFXManager.Instance.sfxAudioSource.volume = sfxSlider.value;
    }
}
