using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

public class LevelManagerMainMenu : MonoBehaviour
{
    string lastLevelName;

    [SerializeField] LoadingScreenManager loading;

    private void Start()
    {
        lastLevelName = PlayerPrefs.GetString("LastLevelName", "Level 1");
        gameObject.SetActive(false);
    }
    public void SaveLastLevelName(string LevelName)
    {
        PlayerPrefs.SetString("LastLevelName", LevelName);
        lastLevelName = LevelName;
    }
    public void OpenLastLevel()
    {
      
        PlayerPrefs.SetString("CurrOpenningLevel", lastLevelName);
        loading.LoadScreen(lastLevelName);
    }
    public void OpenLevel(string LevelName)
    {
        PlayerPrefs.SetString("CurrOpenningLevel", LevelName);
        loading.LoadScreen(LevelName);
    }

   
}