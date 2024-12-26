using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    private Animator _animatorComponent;
    [SerializeField] GameObject loading;
    string openningScene;

    private void Start()
    {
        _animatorComponent = transform.GetComponent<Animator>();  

        // Remove it if you don't want to hide it in the Start function and call it elsewhere
        HideLoadingScreen();
    }

    public void RevealLoadingScreen()
    {
        _animatorComponent.SetTrigger("Reveal");
    }

    public void HideLoadingScreen()
    {
        // Call this function, if you want start hiding the loading screen
        _animatorComponent.SetTrigger("Hide");
    }

    public void OnFinishedReveal()
    {
        StartCoroutine(LoadSceneAsync(openningScene));
    }

    public void OnFinishedHide()
    {
        // TODO: remove it and call your functions 
    }

    internal void LoadScreen(string Name)
    {
        openningScene = Name; 
        Time.timeScale = 1;
    }
    IEnumerator LoadSceneAsync(string Name)
    { 
        loading.SetActive(true);
        yield return new WaitForSeconds(.2f);
        var asyncLoad = SceneManager.LoadSceneAsync(Name, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            if (asyncLoad.progress >= 0.9f)
            { 
                asyncLoad.allowSceneActivation = true;
                yield break;
            }
            yield return null;
        }
        yield return null;
    }
   
}
