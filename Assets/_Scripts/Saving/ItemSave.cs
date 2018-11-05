using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSave
{
    public ItemType itemType;
    public int count;
    
    public ItemSave(ItemType itemType, int count = 1)
    {
        this.itemType = itemType;
        this.count = count;
    }

    public bool IsEmpty { get { return itemType == ItemType.None || count <= 0; } }
}
