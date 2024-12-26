using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonController : MonoBehaviour
{
    Dictionary<string,GameObject> buttonList;
    private void Start()
    {
        buttonList = new Dictionary<string,GameObject>();
    }
    public void addButton(ActionButton actionButton)
    {
        if(!buttonList.ContainsKey(actionButton.ID))
        {
            var x = Instantiate(actionButton.actionButton, transform);
            var button = x.GetComponent<ActionButton>();
            button.ID = actionButton.ID;
            button.OpenUI = actionButton.OpenUI;
            button.Parent = actionButton.Parent;
            button.actionButton = actionButton.actionButton;
            buttonList.Add(actionButton.ID, x);

        }
    }
    public void removeButton(string ID)
    {
        if (buttonList.ContainsKey(ID))
        {
            GameObject deletingButton;
            buttonList.TryGetValue(ID,out deletingButton);
            deletingButton.SetActive(false);
            buttonList.Remove(ID);
        }
    }
}
