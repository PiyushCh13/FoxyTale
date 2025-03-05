using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite checkpointOnSprite;
    [SerializeField] private Sprite checkpointOffSprite;

    public UnityEvent OnTriggerCheckpoint;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTriggerCheckpoint?.Invoke();
            ActivateSprite();
        }
    }

    // public void SetAgentPosition(Agent agent)
    // {
    //     agent.agentData.spawnPoint = transform.position;
    // }

    public void ActivateSprite()
    {
        _spriteRenderer.sprite = checkpointOnSprite;
    }

    public void DeactivateSprite()
    {
        _spriteRenderer.sprite = checkpointOffSprite;
    }
    
}
