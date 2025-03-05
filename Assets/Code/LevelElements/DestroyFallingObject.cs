using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallingObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag("Player")) 
        // {
        //     Agent agent = collision.GetComponent<Agent>();
        //     AgentHealthManager manager = agent.GetComponent<AgentHealthManager>();
        //     manager.OnPitFall?.Invoke();
        //     manager.GetHit();
        // }
    }
}
