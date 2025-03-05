using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public void BackButton()
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
        UIManager.Instance.OpenMenu(mainMenu, this.gameObject);
        UIManager.Instance.AnimateMenu(this.gameObject, Vector3.zero);
        this.gameObject.SetActive(false);
    }
}
