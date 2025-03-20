using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageHit : MonoBehaviour
{
    public Boss boss;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && boss.currentState == Boss.BossState.Shooting)
        {
            if (!other.GetComponent<Player_Controller>().isGrounded)
            {
                boss.TakeHit();
                other.GetComponent<Player_Controller>().KnockBackForce();
            }
        }

    }
}
