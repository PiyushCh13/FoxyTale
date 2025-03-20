using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCollectibleManager : MonoBehaviour
{
    [Header("Health Count")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    public GameObject[] playerHealth;



    [Header("Diamond Count")]
    public int gemCount;

    public UnityEvent OnHit;
    public UnityEvent OnDie;

    [Header("UI")]
    public TMP_Text gemText;

    public Sprite fullHealth;
    public Sprite emptyHealth;

    public GameObject healthSprite;
    public Transform healthParent;

    [Header("Invinsible Counter")]
    public float invinsibleLength;
    private float invinsibleCounter;

    public bool isInvisible = false;

    private SpriteRenderer playerSprite;
    private Animator playerAnim;


    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (invinsibleCounter > 0 && isInvisible)
        {
            invinsibleCounter -= Time.deltaTime;

            if (invinsibleCounter <= 0)
            {
                isInvisible = false;
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
        }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
        }
    }

    public void RemoveHealth()
    {
        if (invinsibleCounter <= 0)
        {
            CurrentHealth -= 1;

            if (currentHealth > 0)
            {
                OnHit?.Invoke();
                isInvisible = true;
                invinsibleCounter = invinsibleLength;
                playerAnim.SetTrigger("Hurt");
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f);
            }
            else
            {
                OnDie?.Invoke();
            }

            SetHealth(currentHealth);
        }
    }

    public void AddHealth()
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + 1, 0, maxHealth);
        SetHealth(currentHealth);
    }

    public void InitializeHealth()
    {
        currentHealth = maxHealth;

        playerHealth = new GameObject[maxHealth];

        foreach (Transform child in healthParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            var life = Instantiate(healthSprite, healthParent.transform);
            playerHealth[i] = healthParent.transform.GetChild(i).gameObject;
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