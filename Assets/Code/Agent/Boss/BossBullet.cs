using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    public AudioClip bossBulletSound;
    public AudioClip playerBulletHit;

    Vector2 bulletMovement;

    void Start()
    {
        SFXManager.Instance.PlaySound(bossBulletSound);
    }

    void Update()
    {
        bulletMovement = new Vector2(-speed * transform.localScale.x * Time.deltaTime, 0);
        transform.Translate(bulletMovement);
    }


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
                player.KnockBackForce();
                SFXManager.Instance.PlaySound(playerBulletHit);
            }
        }
        Destroy(gameObject);
    }
}
