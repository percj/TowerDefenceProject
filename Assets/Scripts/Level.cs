using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] string levelName;
    int starCount;
    [SerializeField] string isLock;
    [SerializeField] List<GameObject> Stars;
    [SerializeField] GameObject Lock;
    [SerializeField] GameObject UnLock;
    [SerializeField] LevelManagerMainMenu levelManagerMainMenu;

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(levelName + "isLock", "True");
        PlayerPrefs.SetInt(levelName + "starCount", 0);
    }
    private void OnEnable()
    {
        LoadData();
    }
    public void LoadData()
    {
        foreach (var item in Stars)
        {
            item.SetActive(true);
        }
        isLock = PlayerPrefs.GetString(levelName+"isLock", isLock);
        starCount = PlayerPrefs.GetInt(levelName+ "starCount", starCount);
        for (int i = 0; i< starCount;i++)
        {
            Stars[i].SetActive(false);
        }
        if (isLock != "True") { Lock.SetActive(true); UnLock.SetActive(false); }
        else { Lock.SetActive(false); UnLock.SetActive(true); }
    }

    public void loadScreen()
    {
        levelManagerMainMenu.OpenLevel(levelName);
    }
}
