using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCompleteScene : MonoBehaviour
{
    public RawImage image;
    void Start()
    {
      MusicManager.Instance.PlayMusic(MusicManager.Instance.menuSong);
    }

    public void GoToMainMenu()
    {
        SceneManagement.Instance.LoadScene(image,SceneList.StartScreen.ToString());
    }
}
