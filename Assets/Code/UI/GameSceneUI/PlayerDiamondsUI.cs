using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDiamondsUI : MonoBehaviour
{
    private TMP_Text diamond_Text;

    public UnityEvent OnTextChange;

    void Awake()
    {
        diamond_Text = GetComponentInChildren<TMP_Text>();
    }

    public void SetPoints(int val)
    { 
        diamond_Text.SetText(val.ToString());
        OnTextChange?.Invoke();
    }

}
