using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public GameObject explosionObject;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            SFXManager.Instance.PlaySound(SFXManager.Instance.enemyExplode);
            Instantiate(explosionObject, collision.transform.position, Quaternion.identity);
            transform.parent.GetComponent<Player_Controller>().KnockBackForce();
            Destroy(collision.transform.parent.gameObject);
        }

        if(collision.gameObject.CompareTag("BossHead"))
        {
            Boss boss = GameObject.FindFirstObjectByType<Boss>();

            if (boss != null)
            {
                if(boss.currentState == Boss.BossState.Shooting)
                {
                    boss.TakeHit();
                    transform.parent.GetComponent<Player_Controller>().KnockBackForce();
                }
                else
                  return;
            }
        }
    }

}
