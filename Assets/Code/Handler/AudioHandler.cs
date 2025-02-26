using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (MusicManager.Instance.musicSource.isPlaying) 
        {
            MusicManager.Instance.musicSource.Stop();
            MusicManager.Instance.PlayMusic(MusicManager.Instance.mainLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
