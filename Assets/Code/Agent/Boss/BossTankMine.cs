using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    public GameObject explosion;
    public int DestroyTime = 8;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Controller player = other.GetComponent<Player_Controller>();
            PlayerCollectibleManager playerCollectibleManager = other.GetComponent<PlayerCollectibleManager>();

            if (playerCollectibleManager != null)
            {
                if (playerCollectibleManager.isInvisible)
                    return;

                playerCollectibleManager.RemoveHealth();
                Instantiate(explosion, transform.position, transform.rotation);
                player.KnockBackForce();
                SFXManager.Instance.PlaySound(SFXManager.Instance.enemyExplode);
            }

            Destroy(gameObject);
        }
    }

    public void Explode()
    {
        Destroy(gameObject);
        SFXManager.Instance.PlaySound(SFXManager.Instance.enemyExplode);
        Instantiate(explosion, transform.position, transform.rotation);
    }


}
