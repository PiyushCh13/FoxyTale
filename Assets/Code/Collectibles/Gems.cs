using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Gems : Pickable
{
    public GameObject explosionObject;
    [SerializeField] public AudioClip gemPickup;

    public override void Pickup(Player_Controller player)
    {
        PlayerCollectibleManager manager = player.GetComponent<PlayerCollectibleManager>();

        if (manager != null)
        {
            manager.gemCount++;
            manager.gemText.text = manager.gemCount.ToString();
            SFXManager.Instance.PlaySound(gemPickup);
            Instantiate(explosionObject, transform.position, Quaternion.identity);
        }
    }
}
