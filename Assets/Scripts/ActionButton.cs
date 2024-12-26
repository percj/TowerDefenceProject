using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public string ID;
    public GameObject actionButton;
    [HideInInspector] public GameObject OpenUI;
    [HideInInspector] public GameObject Parent;

    public void ButtonClicked()
    {
        OpenUI.GetComponent<ButtonUIController>().parentObject = Parent;
        OpenUI.transform.parent.gameObject.SetActive(true);
    }
}