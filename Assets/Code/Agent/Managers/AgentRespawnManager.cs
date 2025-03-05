using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRespawnManager : MonoBehaviour
{
    private Checkpoint[] checkpoints;
    public Vector3 currentSpawnPoint;

    void Start()
    {
        checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
    }

    public void DeactivateAllSpawnPoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].DeactivateSprite();
        }
    }

    public void SetSpawnPoint(Player_Controller controller)
    {
        currentSpawnPoint = controller.agentData.spawnPoint;
    }

    public void InitializeSpawnPoint(Vector3 spawnPoint , Vector3 toSpawnPoint)
    {
        spawnPoint = toSpawnPoint;
        currentSpawnPoint = spawnPoint;
    }

    public void RespawnPlayer(Player_Controller controller)
    {
         controller.transform.position = currentSpawnPoint;
    }
}
