using UnityEngine;

public class BossPlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerCollectibleManager>().RemoveHealth();
            other.GetComponent<Player_Controller>().KnockBackForce();
        }
    }
}
