using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] public LevelList nextLevel, currentLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySound(SFXManager.Instance.levelVictory);

            if (currentLevel == LevelList.BossLevel)
            {
                SceneManagement.Instance.LoadScene(collision.gameObject.GetComponent<Player_Controller>().rawImage, SceneList.GameComplete.ToString());
            }

            else
            {
                Debug.Log("Level End Triggered");
                Player_Controller player = collision.gameObject.GetComponent<Player_Controller>();
                PlayerCollectibleManager playerCollectibleManager = collision.gameObject.GetComponent<PlayerCollectibleManager>();

                LevelDataSO currentLeveldata = LevelDataManager.Instance.GetLevelData(currentLevel);

                if (currentLeveldata != null)
                {
                    currentLeveldata.diamondsCollected = Mathf.Clamp(playerCollectibleManager.gemCount, 0, currentLeveldata.diamondsTotal);
                    currentLeveldata.isUnlocked = true;
                }

                Debug.Log("Current Level Data: " + currentLeveldata.diamondsCollected + " / " + currentLeveldata.isUnlocked);
                
                LevelDataSO nextLevelData = LevelDataManager.Instance.GetLevelData(nextLevel);
                if (nextLevelData != null)
                {
                    nextLevelData.isUnlocked = true;
                }

                Debug.Log("Next Level: " + nextLevelData.diamondsCollected + " / " + nextLevelData.isUnlocked);

                LevelDataManager.Instance.UpdateLevelData(currentLevel, currentLeveldata);

                LevelDataManager.Instance.UpdateLevelData(nextLevel, nextLevelData);

                GameManager.Instance.AddUnlockLevel(nextLevel);

                GameManager.Instance.SaveLevelData(currentLevel);

                if (player != null)
                {
                    SceneManagement.Instance.LoadScene(player.rawImage, SceneList.LevelSelection.ToString());
                }
            }
        }
    }


}
