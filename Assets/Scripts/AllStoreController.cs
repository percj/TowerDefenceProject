using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class AllStoreController : MonoBehaviour
{
    [SerializeField] List<StoreItem> Heads;
    [SerializeField] List<StoreItem> Bodys;
    // Start is called before the first frame update
    public void UIRefresh(StoreItemType storeItemType)
    {
        if(storeItemType == StoreItemType.Body)Bodys.ForEach(x => { x.Equip = "False"; x.SaveData();  x.LoadData(); });
        else Heads.ForEach(x => {x.Equip = "False"; x.SaveData(); x.LoadData();});
    }
}
