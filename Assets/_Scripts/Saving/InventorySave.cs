using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySave
{
    public ItemSave[] items;

    // Equipment (index of items array)
    public int weapon = -1;
    public int armor = -1;
    public int accessory = -1;
    public int boots = -1;
    public int hat = -1;

    // Default inventorySize
    public InventorySave(int size = 20)
    {
        items = new ItemSave[size];
        for (int i = 0; i < size; i++)
            items[i] = null;
    }

    public void Resize(int size)
    {
        // Check that new inventory size is larger than old one
        if (size <= items.Length)
            return;

        var newItems = new ItemSave[size];
        for (int i = 0; i < size; i++)
        {
            newItems[i] = i < items.Length ? items[i] : null;
        }
        items = newItems;
    }

    public bool AddItem(ItemSave itemSave)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i].IsEmpty)
            {
                items[i] = itemSave;
                return true;
            }
        }
        return false;
    }

    public void Swap(int index1, int index2)
    {
        if (index1 < items.Length && index2 < items.Length)
            items.Swap(index1, index2);
    }

    private ItemSave GetEquipment(int index)
    {
        if (index >= 0 && index < items.Length)
            return items[index];
        return null;
    }

    public ItemSave GetWeapon()
    {
        return GetEquipment(weapon);
    }
}
