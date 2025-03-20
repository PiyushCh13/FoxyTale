using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    public GameObject bossBattle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossBattle.SetActive(true);
        }
    }

}
