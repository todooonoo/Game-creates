using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour {

    [SerializeField]
    protected ItemSave itemSave;

    public ItemType ItemType { get { return itemSave.itemType; } }

	// Use this for initialization
	void Start ()
    {
		
	}
}
