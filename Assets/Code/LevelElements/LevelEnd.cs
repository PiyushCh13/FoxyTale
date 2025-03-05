using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] public LevelList nextLevel,currentLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Agent agent = collision.gameObject.GetComponent<Agent>();

            // if (agent != null)
            // {
            //     SceneManagement.Instance.LoadScene(agent.rawImage, SceneList.LevelSelection.ToString());
            // }

            // LevelDataSO currentLeveldata = LevelDataManager.Instance.GetLevelData(currentLevel);
            // currentLeveldata.diamondsCollected = GameManager.Instance.collectedGems;

            // LevelDataSO nextLeveldata = LevelDataManager.Instance.GetLevelData(nextLevel);
            // nextLeveldata.isUnlocked = true;

            // GameManager.Instance.AddUnlockLevel(nextLevel);
        }
    }
}
