using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class PlayerDiamondManager : MonoBehaviour
{
    public UnityEvent<int> OnDiamondValueChange;
    public UnityEvent OnDiamondPickup;

    private int collected_Diamond = 0;

    public int Collected_Diamonds
    {
        get
        {
            return collected_Diamond;
        }

        private set
        {
            collected_Diamond = value;
        }
    }

    void Start()
    {
        OnDiamondValueChange?.Invoke(Collected_Diamonds);
    }

    public void Add(int Amount)
    {
        Collected_Diamonds += Amount;
        OnDiamondPickup?.Invoke();
        OnDiamondValueChange?.Invoke(Collected_Diamonds);
        GameManager.Instance.CollectedGemsCounter(Collected_Diamonds);
    }

    public void PlaySoundOnPickup()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.gemPickup);
    }
}
