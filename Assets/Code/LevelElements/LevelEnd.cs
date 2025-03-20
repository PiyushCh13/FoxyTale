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
                Player_Controller player = collision.gameObject.GetComponent<Player_Controller>();
                PlayerCollectibleManager playerCollectibleManager = collision.gameObject.GetComponent<PlayerCollectibleManager>();

                if (player != null)
                {
                    SceneManagement.Instance.LoadScene(player.rawImage, SceneList.LevelSelection.ToString());
                }

                LevelDataSO currentLeveldata = new LevelDataSO();
                currentLeveldata.diamondsCollected = Mathf.Clamp(0, playerCollectibleManager.gemCount, currentLeveldata.diamondsTotal);
                currentLeveldata.isUnlocked = true;

                LevelDataSO nextLevelData = new LevelDataSO();
                nextLevelData.isUnlocked = true;

                LevelDataManager.Instance.UpdateLevelData(currentLevel, currentLeveldata);

                LevelDataManager.Instance.UpdateLevelData(nextLevel, nextLevelData);

                GameManager.Instance.AddUnlockLevel(nextLevel);
                GameManager.Instance.SaveLevelData(currentLevel);
            }
        }
    }


}
