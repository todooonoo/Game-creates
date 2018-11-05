using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager> {

    [SerializeField]
    private GameItem[] itemPrefabs;
    
    public GameItem GetItemPrefab(ItemType itemType)
    {
        for(int i = 0; i < itemPrefabs.Length; i++)
        {
            if (itemPrefabs[i].ItemType == itemType)
                return itemPrefabs[i];
        }
        return null;
    }
}
