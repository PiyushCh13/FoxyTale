using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealthManager : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    public UnityEvent OnHit;
    public UnityEvent OnHealthAdd;
    public UnityEvent OnDie;
    public UnityEvent OnPitFall;
    public UnityEvent<int> OnInitializeMaxHealth;
    public UnityEvent<int> OnHealthValueChange;

    public int CurrentHealth { get { return currentHealth; }
        set
        {
            currentHealth = value; 
            OnHealthValueChange?.Invoke(currentHealth); 
        }
    }

    public void GetHit()
    {
        CurrentHealth -= 1;

        if (currentHealth > 0)
        {
            OnHit?.Invoke();
        }
        else
        {
            OnDie?.Invoke();
        }
    }

    public void AddHealth() 
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + 1, 0, maxHealth);
        OnHealthAdd?.Invoke();
    }

    public void InitializeHealth(int health) 
    {
        currentHealth = health;
        OnInitializeMaxHealth?.Invoke(health);
    }

    public void PlaySoundOnPickup()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.healthPickup);
    }
}