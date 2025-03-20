using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnManager : MonoBehaviour
{
    private Checkpoint[] checkpoints;
    public Checkpoint currentCheckpoint;
    public Vector3 currentSpawnPoint;
    public Vector3 initalSpawnPoint = new Vector3(0, 1, 0);

    void Start()
    {
        checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
        currentSpawnPoint = initalSpawnPoint;
        
    }

    public void DeactivateAllSpawnPoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].DeactivateSprite();
        }
    }

    public void RespawnPlayer(Player_Controller controller)
    {
        controller.transform.position = currentSpawnPoint;
    }

    public void SetSpawnPoint(Checkpoint checkpoint)
    {
        currentSpawnPoint = checkpoint.transform.position;
        currentCheckpoint = checkpoint;
    }
}
