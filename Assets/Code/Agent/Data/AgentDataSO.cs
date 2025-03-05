using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO" , menuName = "Data/Player",order = 1)]
public class AgentDataSO : ScriptableObject
{
    [Header("Health Data")]
    [Space]
    public int health;

    [Header("Spawn Data")]
    [Space]
    public Vector3 spawnPoint;

    [Header("Movement Data")]
    [Space]
    public float agentSpeed;


    [Header("Jump Data")]
    [Space]
    public float jumpForce;
    public float lowJumpMultiplier;
    public float gravity_Modifier;
    public float doubleJumpForce;
    [Space]

    [Range(0, 3)] public int maxJumps;
    public int jumpCount;

    [Header("MoveData")]
    [Space]
    public Vector2 currentVelocity;

    [Header("Camera Controls")]
    public float minZoom;
    public float maxZoom; 
}
