using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipController : MonoBehaviour {

    [SerializeField]
    private Transform weaponEquipTransform;
    private InventorySave inventory;
    private Player3D player;

	private void Start ()
    {
        inventory = SaveManager.Instance.GameSave.playerSave.inventory;
        player = GetComponent<Player3D>();

        // Load equipment
        LoadWeapon();
	}

    private void LoadWeapon()
    {
        var weapon = inventory.GetWeapon();

        if (weapon != null)
        {
            var weaponPrefab = ItemManager.Instance.GetItemPrefab(weapon.itemType);
            if (weaponPrefab)
            {
                var newWeapon = weaponPrefab.Spawn(weaponEquipTransform);
                newWeapon.GetComponent<GameWeapon>().OnEquip(player);
            }
        }
    }
}
