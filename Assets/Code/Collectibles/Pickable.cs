using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    public LevelDataSO levelOne;
    public LevelDataSO levelTwo;
    public LevelDataSO levelThree;
    public LevelDataSO bossLevel;
    
    public abstract void Pickup(Player_Controller player);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup(collision.GetComponent<Player_Controller>());
            Destroy(gameObject);
        }
    }
}
