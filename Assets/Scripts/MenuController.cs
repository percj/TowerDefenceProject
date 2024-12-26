using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void StoreClick()
    {
        animator.SetTrigger("Store");
    }
    public void StoreExitClick()
    {
        animator.SetTrigger("StoreExit");
    }

    public void StoreSelectedClick()
    {
        animator.SetTrigger("Selected");
    }
}
