using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] List<GameObject> HeadSkin;
    [SerializeField] List<GameObject> BodySkin;


    // Start is called before the first frame update
    void Start()
    {
        HeadSkin.ForEach(x => x.SetActive(false));
        BodySkin.ForEach(x => x.SetActive(false));
        LoadData();
    }


    private void SaveData()
    {
       var openedHeadSkin=  HeadSkin.Where(x => x.active == false).First();
       var openedBodySkin= BodySkin.Where(x => x.active == false).First();
        PlayerPrefs.SetString("CurrSkinForBody", openedBodySkin.name);
        PlayerPrefs.SetString("CurrSkinForHead", openedHeadSkin.name);
    }

    public void LoadData()
    {
       var openedHeadSkin = PlayerPrefs.GetString("CurrSkinForHead", "Head_01a");
       var openedBodySkin = PlayerPrefs.GetString("CurrSkinForBody", "Body_01a");

        HeadSkin.ForEach(x => { if (x.name == openedHeadSkin) x.SetActive(true); });
        BodySkin.ForEach(x => { if (x.name == openedBodySkin) x.SetActive(true); });
    }
    public void EquipItem(StoreItemType type,string skinName)
    {
        if (type == StoreItemType.Body) EquipBodySkin(skinName);
        else if (type == StoreItemType.Head) EquipHeadSkin(skinName);
    }

    void EquipHeadSkin(string skinName)
    {
        PlayerPrefs.SetString("CurrSkinForHead", skinName);
        HeadSkin.ForEach(x => x.SetActive(false));
        HeadSkin.ForEach(x => { if (x.name == skinName) x.SetActive(true); });
    }
    void EquipBodySkin(string skinName)
    {
        PlayerPrefs.SetString("CurrSkinForBody", skinName);
        BodySkin.ForEach(x => x.SetActive(false));
        BodySkin.ForEach(x => { if (x.name == skinName) x.SetActive(true); });
    }
}
