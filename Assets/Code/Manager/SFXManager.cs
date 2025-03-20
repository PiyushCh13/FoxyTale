using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SFXManager : Singleton<SFXManager>
{
    public AudioSource sfxAudioSource;
    [SerializeField] public AudioClip mapMoveSound;
    [SerializeField] public AudioClip clickSound;
    [SerializeField] public AudioClip levelSelected;
    [SerializeField] public AudioClip enemyExplode;

    [SerializeField] public AudioClip levelVictory;
    public AudioClip playerHurt;

    void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();
        sfxAudioSource.enabled = true;
    }

    private void Update()
    {

    }
    public void PlaySound(AudioClip clip)
    {
        sfxAudioSource.clip = clip;
        sfxAudioSource.Play();

    }


}
