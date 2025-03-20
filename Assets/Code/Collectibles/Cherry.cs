using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : Pickable
{
    [SerializeField] public AudioClip cherryPickup;
    public override void Pickup(Player_Controller player)
    {
        PlayerCollectibleManager manager = player.GetComponent<PlayerCollectibleManager>();

        if (manager != null)
        {
            manager.AddHealth();
            SFXManager.Instance.PlaySound(cherryPickup);
        }
    }
}
