using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerCollectibleManager>().isInvisible)
                return;

            other.GetComponent<PlayerCollectibleManager>().RemoveHealth();
            other.GetComponent<Player_Controller>().KnockBackForce();

            SFXManager.Instance.PlaySound(SFXManager.Instance.playerHurt);
        }

    }
}
