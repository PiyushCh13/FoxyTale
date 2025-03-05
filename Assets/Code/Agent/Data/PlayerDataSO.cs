using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO" , menuName = "Data/Player",order = 1)]
public class PlayerDataSO : ScriptableObject
{
    [Header("Health Data")]
    [Space]
    public int health;

    [Header("Spawn Data")]
    [Space]
    public Vector3 spawnPoint;
}
