using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Sprite fullHealth;
    public Sprite emptyHealth;

    public GameObject healthSprite;

    public GameObject[] playerHealth;
    private void Start()
    {

    }
    public void Initialize(int maxHealth)
    {
        playerHealth = new GameObject[maxHealth];

        foreach(Transform child in transform) 
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < maxHealth; i++)
        {
            var life = Instantiate(healthSprite, transform);
            playerHealth[i] = transform.GetChild(i).gameObject;
        }
    }

    public void SetHealth(int currentHealth)
    {
        for (int i = 0; i < playerHealth.Length; i++)
        {
            if (i < currentHealth)
            {
                playerHealth[i].GetComponent<Image>().sprite = fullHealth;
            }
            else
            {
                playerHealth[i].GetComponent<Image>().sprite = emptyHealth;
            }
        }
    }
}
